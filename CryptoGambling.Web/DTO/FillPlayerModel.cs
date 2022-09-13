using CryptoGambling.Data.Funds;
using CryptoGambling.Data.Users;
using CryptoGambling.Web.Models;

namespace CryptoGambling.Web.DTO
{
    public static class FillPlayerModel
    {
        public static void Fill(PlayerModel pm, User user)
        {
            pm.BtcBalance = FormatDeciml(user?.Wallet?.BtcBalance);
            pm.LtcBalance = FormatDeciml(user?.Wallet?.LtcBalance);
            pm.DogeBalance = FormatDeciml(user?.Wallet?.DogeBalance);

            pm.BtcDepositeAddress = user?.Wallet?.BtcAddress ?? "";
            pm.LtcDepositeAddress = user?.Wallet?.LtcAddress ?? "";
            pm.DogeDepositeAddress = user?.Wallet?.DogeAddress ?? "";

            pm.Deposites = FillDeposites(user?.Deposites ?? null);
            pm.Withdrawals = user?.Withdrawals ?? null;

        }

        private static decimal FormatDeciml(decimal? balance)
        {
            decimal bal = balance ?? 0m;
            return Math.Round(bal, 8);
        }

        private static List<DepositeModel>? FillDeposites(List<Deposite>? deposites)
        {
            var result = new List<DepositeModel>();
            if (deposites is not null)
            {
                foreach (var i in deposites)
                {
                    result.Add(ConvertDepositeToDepositeModel(i));
                }
            }
            return result;
        }

        public static DepositeModel ConvertDepositeToDepositeModel(Deposite deposite)
        {
            var depositeModel = new DepositeModel();
            depositeModel.Hash = deposite?.Hash ?? "";
            depositeModel.Amount = deposite?.Amount ?? 0;
            depositeModel.Currency = deposite?.Curreny ?? Currency.Btc;

            return depositeModel;
        }
    }
}
