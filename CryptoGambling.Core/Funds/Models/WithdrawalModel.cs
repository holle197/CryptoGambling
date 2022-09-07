using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Funds.Models
{
    public class WithdrawalModel
    {
        public decimal Amount { get; set; }
        public string? DestinationAddress { get; set; }
    }
}
