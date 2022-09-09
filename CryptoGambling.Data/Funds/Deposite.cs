﻿using CryptoGambling.Data.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Data.Funds
{
    public class Deposite
    {
        public int Id { get; set; }
        public string? DepositeHash { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

    }
}