using Microsoft.AspNetCore.Mvc;
using VkDataBase.Repositories;
using VkDataBase.DtoModels;

namespace Vk_case.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepository)
        {
            _userRepo = userRepository;
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetUser(string login)
        {
            IActionResult response;
            try
            {
                var userDto = await _userRepo.GetUserAsync(login);
                response = Ok(userDto);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            IActionResult response;
            try
            {
                var userListDto = await _userRepo.GetUsersAsync();
                response = Ok(userListDto);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDtoModel userDto)
        {
            
            IActionResult response;
            try
            {
                await _userRepo.CreateUserAsync(userDto);
                response = StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpDelete("{login}")]
        public async Task<IActionResult> DeleteUser(string login)
        {
            IActionResult response;

            try
            {
                await _userRepo.DeleteUserAsync(login);
                response = StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return response;
        }
    }
}