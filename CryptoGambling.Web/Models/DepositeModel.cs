using CryptoGambling.Data.Funds;

namespace CryptoGambling.Web.Models
{
    public class DepositeModel
    {
        public string Hash { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
