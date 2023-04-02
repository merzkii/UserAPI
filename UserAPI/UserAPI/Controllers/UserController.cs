using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.DTO;
using UserAPI.Interfaces;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserRepository UserRepository { get; set; }
        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRegisterDTO user)
        {
            var create=UserRepository.CreateUser(user);
            return Ok(create);
        }
        [HttpGet]
        public async Task<IActionResult> GetUser(AuthorizeUserDTO user)
        {
            var get=UserRepository.GetUser(user);
            return Ok(get);
        }
        //[HttpGet("myendpoint")]
        //public async Task<IActionResult> MyEndpoint()
        //{
        //    int userId = int.Parse(User.Claims.First(c => c.Type == "Id").Value);
        //    string name = User.Claims.First(c => c.Type == "UserName").Value;
        //    string email = User.Claims.First(c => c.Type == "email").Value;


        //    return 
        //}
    }
}
