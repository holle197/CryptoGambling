using CryptoGambling.Data.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Data.Funds
{
    public class Withdrawal
    {
        public int Id { get; set; }
        public string? Hash { get; set; }
        [Column(TypeName = "decimal(18, 8)")]
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public virtual User? User { get; set; }
    }
}
