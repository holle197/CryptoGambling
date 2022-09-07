using CryptoGambling.Core.Games.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Games.Models
{
    public class GameInput
    {
        public decimal BetAmount { get; set; }
        public decimal WalletBalance { get; set; }
        public Difficulty Difficulty { get; set; }
        public Currencies Currency { get; set; }
    }
}
