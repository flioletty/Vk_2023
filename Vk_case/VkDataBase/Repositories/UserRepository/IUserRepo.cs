using VkDataBase.DtoModels;

namespace VkDataBase.Repositories
{
    public interface IUserRepo
    {
        Task CreateUserAsync(UserDtoModel UserDto);
        Task DeleteUserAsync(string login);
        Task<UserDtoModel> GetUserAsync(string login);
        Task<List<UserDtoModel>> GetUsersAsync();
    }
}
