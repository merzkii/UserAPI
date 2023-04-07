﻿using UserAPI.Models;

namespace UserAPI.DTO
{
    public class UserRegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int walletId { get; set; }
       
    }
}
