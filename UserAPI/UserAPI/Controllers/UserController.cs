using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAPI.DTO;
using UserAPI.Interfaces;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserRepository UserRepository { get; set; }
        public IWalletRepository WalletRepository { get; set; }
        public UserController(IUserRepository userRepository,IWalletRepository walletRepository)
        {
            UserRepository = userRepository;
            WalletRepository = walletRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRegisterDTO user)
        {
            var create=await UserRepository.CreateUser(user);
            return Ok(create);
        }
        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery]AuthorizeUserDTO user)
        {
            var get=await UserRepository.GetUser(user);
           
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
