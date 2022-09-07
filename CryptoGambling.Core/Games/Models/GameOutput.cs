using CryptoGambling.Core.Games.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Games.Models
{
    public class GameOutput
    {
        public string? GameName { get; set; }
        public Currencies Currency { get; set; }
        public bool IsGameWinning { get; set; }
        public decimal Quote { get; set; }
        public decimal Amount { get; set; }
        // profit can be positeve or negative
        public decimal Profit { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
