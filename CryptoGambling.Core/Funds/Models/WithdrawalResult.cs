using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Funds.Models
{
    public class WithdrawalResult
    {
        public decimal Amount { get; set; }
        public string? TxHash { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
