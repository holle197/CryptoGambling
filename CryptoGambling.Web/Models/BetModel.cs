using CryptoGambling.Core.Games.Enums;
using CryptoGambling.Data.Funds;

namespace CryptoGambling.Web.Models
{
    public class BetModel
    {
        public decimal BetAmount { get; set; }
        public decimal WalletBalance { get; set; }
        public Difficulty Difficulty { get; set; }
        public Currency Currency { get; set; }

        public int IntLuckyField { get; set; }
        public List<int> ListIntLuckyField { get; set; } = new();
    }
}
