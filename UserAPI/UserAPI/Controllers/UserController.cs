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
        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
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


        private JwtSecurityToken GenerateJwtToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("ABCD");
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim("Id", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName",user.LastName),
            new Claim("UserName", user.UserName),
            new Claim("email", user.Email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return (JwtSecurityToken)token;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);


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
