﻿namespace UserAPI.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }
    }
}
