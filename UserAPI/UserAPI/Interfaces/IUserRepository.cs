using System.IdentityModel.Tokens.Jwt;
using UserAPI.DTO;

namespace UserAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateUser(UserRegisterDTO user);
        Task<User> GetUser(AuthorizeUserDTO user);
    }
}
