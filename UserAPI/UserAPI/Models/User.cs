using System.Text.Json.Serialization;
using UserAPI.Models;

namespace UserAPI
{
    public class User
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Wallet Wallet { get; set; }
        public int WalletId { get; set; }
       
    }
}
