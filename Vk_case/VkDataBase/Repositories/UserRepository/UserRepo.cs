using VkDataBase.DtoModels;
using VkDataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace VkDataBase.Repositories
{
    public class UserRepo: BaseRepo, IUserRepo
    {

        public UserRepo(VkCaseDbContext context) : base(context) { }

        public UserDtoModel GetDtoFromUser(User user)
        {
            var userDto = new UserDtoModel()
            {
                Login = user.Login,
                Password = user.Password,
                CodeGroup = user.Group.Code,
                CodeState = user.State.Code,
                CreatedDate = user.CreatedDate,
                GroupDescription = user.Group.Description,
                StateDescription = user.State.Description
            };
            return userDto;
        }

        public User GetUserFromDto(UserDtoModel userDto)
        {
            if (userDto.CodeState != UserStateCode.Active && userDto.CodeState != UserStateCode.Blocked)
                throw new Exception("The user status is incorrectly specified");
            if (userDto.CodeGroup != UserGroupCode.Admin && userDto.CodeGroup != UserGroupCode.User)
                throw new Exception("The user's group is specified incorrectly");
            var user = new User()
            {
                Login = userDto.Login,
                Password = userDto.Password,
                Group = new UserGroup()
                {
                    Code = userDto.CodeGroup,
                    Description = userDto.GroupDescription
                },
                State = new UserState()
                {
                    Code = userDto.CodeState,
                    Description = userDto.StateDescription
                }
            };
            return user;
        }

        public async Task CreateUserAsync(UserDtoModel userDto)
        {
            var user = GetUserFromDto(userDto);
            if ((user.Group.Code == UserGroupCode.Admin && !_context.Users.Any(u => u.Group.Code == UserGroupCode.Admin && u.State.Code==UserStateCode.Active)) 
                || user.Group.Code != UserGroupCode.Admin)
            {
                var userInDb = _context.Users.Include(p => p.Group).Include(p => p.State).Where(p => p.Login == user.Login).FirstOrDefault();
                if (userInDb != null)
                {
                    if (userInDb.State.Code == UserStateCode.Active)
                        throw new Exception("User with such login already exists");
                    userInDb.State.Code = UserStateCode.Active;
                    userInDb.Password = user.Password;
                    userInDb.Group = user.Group;
                    userInDb.CreatedDate = user.CreatedDate;
                    _context.Entry(userInDb).State = EntityState.Modified;
                }
                else
                {
                    await _context.Users.AddAsync(user);
                }
                await _context.SaveChangesAsync();
            }else throw new Exception("User with state admin already exists");
        }
        public async Task DeleteUserAsync(string login)
        {
            var userInDb = _context.Users.Include(p => p.State).Where(p => p.Login == login && p.State.Code == UserStateCode.Active).FirstOrDefault();
            if(userInDb == null)
                throw new Exception("User with such login doesn't exist");
            userInDb.State.Code = UserStateCode.Blocked;
            _context.Entry(userInDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<UserDtoModel> GetUserAsync(string login)
        {
            var userInDb = _context.Users.Include(p => p.Group).Include(p => p.State).Where(p => p.Login == login && p.State.Code == UserStateCode.Active).FirstOrDefault();
            if (userInDb == null)
                throw new Exception("User with such login doesn't exist");
            return GetDtoFromUser(userInDb);
        }

        public async Task<List<UserDtoModel>> GetUsersAsync()
        {
            var users = await _context.Users.Include(p => p.Group).Include(p => p.State).ToListAsync();
            var res = new List<UserDtoModel>();
            foreach (var user in users)
            {
                res.Add(GetDtoFromUser(user));
            }
            return res;
        }
    }
}
