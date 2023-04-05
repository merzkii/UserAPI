using UserAPI.Models;

namespace UserAPI.Interfaces
{
    public interface IWalletRepository
    {
       
        Task<int> Deposit(int userId,decimal amount);
        Task<int> Witdraw(int userId,decimal amount);
    }
}
