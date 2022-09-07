using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Funds.Models
{
    public class DepositeResult
    {
        public decimal Balance { get; set; }
        public string? TxHash { get; set; }
    }
}
