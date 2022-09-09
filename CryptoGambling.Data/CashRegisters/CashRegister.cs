using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Data.CashRegisters
{
    public class CashRegister
    {
        public int Id { get; set; }
        public decimal EarnedBalance { get; set; }
        public decimal SharedBalance { get; set; }
    }
}
