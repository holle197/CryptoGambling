using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Data.CashRegisters
{
    public class CashRegister
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 8)")]
        public decimal BtcEarnedBalance { get; set; }
        [Column(TypeName = "decimal(18, 8)")]
        public decimal BtcSharedBalance { get; set; }
        [Column(TypeName = "decimal(18, 8)")]
        public decimal LtcEarnedBalance { get; set; }
        [Column(TypeName = "decimal(18, 8)")]
        public decimal LtcSharedBalance { get; set; }
        [Column(TypeName = "decimal(18, 8)")]
        public decimal DogeEarnedBalance { get; set; }
        [Column(TypeName = "decimal(18, 8)")]
        public decimal DogeSharedBalance { get; set; }
    }
}
