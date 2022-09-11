using CryptoGambling.Data.Funds;

namespace CryptoGambling.Web.Models
{
    public class PlayerModel
    {
        public string BtcDepositeAddress { get; set; } = string.Empty;
        public string LtcDepositeAddress { get; set; } = string.Empty;
        public string DogeDepositeAddress { get; set; } = string.Empty;

        public decimal BtcBalance { get; set; }
        public decimal LtcBalance { get; set; }
        public decimal DogeBalance { get; set; }

        public List<Deposite>? Deposites { get; set; }
        public List<Withdrawal>? Withdrawals { get; set; }

        // TODO
        // Add referral balances,deposites,withdrawals etc..

    }
}
