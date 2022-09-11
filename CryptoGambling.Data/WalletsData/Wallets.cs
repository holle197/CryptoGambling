﻿using CryptoGambling.Data.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Data.WalletsData
{
    public class Wallets
    {
        public int Id { get; set; }
        public string? BtcAddress { get; set; }
        public string? LtcAddress { get; set; }
        public string? DogeAddress { get; set; }

        public decimal BtcBalance { get; set; }
        public decimal LtcBalance { get; set; }
        public decimal DogeBalance { get; set; }

        public decimal BtcReferredBalance { get; set; }
        public decimal LtcReferredBalance { get; set; }
        public decimal DogeReferredBalance { get; set; }
        public virtual User? User { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

    }
}
