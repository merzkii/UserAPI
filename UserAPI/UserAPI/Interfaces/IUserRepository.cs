using System.IdentityModel.Tokens.Jwt;
using UserAPI.DTO;

namespace UserAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateUser(UserRegisterDTO user);
        Task<JwtSecurityToken> GetUser(AuthorizeUserDTO user);
    }
}
