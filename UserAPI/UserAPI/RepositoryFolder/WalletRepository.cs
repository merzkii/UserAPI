using Dapper;
using System.Data.SqlClient;
using UserAPI.Interfaces;
using UserAPI.Models;

namespace UserAPI.RepositoryFolder
{
    public class WalletRepository:IWalletRepository
    {
        private readonly IConfiguration Config;
        public WalletRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<int> Deposit(int userId, decimal amount)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            {
                connection.Open();

                var sql = "UPDATE Wallet SET Balance = Balance + @Amount WHERE UserId = @UserId";
                var deposit = await connection.ExecuteAsync(sql, new { UserId = userId, Amount = amount });
                return  deposit;
                connection.Close();
            }


        }
        public async Task<int> Witdraw(int userId,decimal amount)
        {
            using var connection = new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            var balance = new Wallet().Balance;

            if (balance < amount)
            {
                throw new Exception("Insufficient funds");
            }

            var sql = "UPDATE Wallets SET Balance = Balance - @Amount WHERE UserId = @UserId";
            var withdraw=await connection.ExecuteAsync(sql, new { UserId = userId, Amount = amount });
            return withdraw;
            
        }
    }

}
