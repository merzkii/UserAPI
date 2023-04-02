using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAPI.DTO;
using UserAPI.Interfaces;
using Dapper;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace UserAPI.RepositoryFolder
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration Config;
        public UserRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<int> CreateUser(UserRegisterDTO user)
        {
            var a = new
            {
                FirstName = user.FirstName,
                UserName = user.UserName,
                LastName = user.LastName,
                Email = user.Email,
                Password = "Hello"
            };
            var serialized = JsonConvert.SerializeObject(a);

            

            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            string sql = "INSERT INTO Users (UserName,FirstName,LastName, Email, Password) VALUES (@UserName,@FirstName,@LastName, @Email, @Password)";
            //string hashedPassword = HashPassword(user.Password);
            var create=await connection.ExecuteAsync(sql, new { FirstName = user.FirstName, UserName=user.UserName,
                LastName=user.LastName, Email = user.Email, Password = "Hello" });
            return create;

        }
        public async Task<IEnumerable<User>> SelectAllUsers(SqlConnection connection)
        {
            
            var all = await connection.QueryAsync<User>("Select * from Users");
            return all.ToList();
        }

        public async Task<JwtSecurityToken> GetUser(AuthorizeUserDTO user)
        {

             using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            string sql = "SELECT * FROM Users WHERE UserName = @UserName";
                var users = await connection.QuerySingleOrDefaultAsync<User>(sql, new { UserName = user.UserName });
                if (user != null && VerifyPassword(user.UserName, user.Password))
                {
                    JwtSecurityToken token = GenerateJwtToken(users);
                    return token;
                }
                return null;
            


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
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        //private void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hashmac = new HMACSHA512())
        //    {
        //        passwordSalt = hashmac.Key;
        //        passwordHash = hashmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}
    }
}
