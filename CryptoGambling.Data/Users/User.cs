using CryptoGambling.Data.Funds;
using CryptoGambling.Data.WalletsData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Data.Users
{
    public class User
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? ReferralLink { get; set; }
        public string? ReferredBy { get; set; }
        public virtual Wallets? Wallet { get; set; }
        public List<Deposite>? Deposites { get; set; }
        public List<Withdrawal>? Withdrawals { get; set; }


    }
}
