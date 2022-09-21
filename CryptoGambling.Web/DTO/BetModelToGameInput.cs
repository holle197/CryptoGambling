using CryptoGambling.Core.Games.Models;
using CryptoGambling.Data.Funds;
using CryptoGambling.Web.Models;

namespace CryptoGambling.Web.DTO
{
    public static class BetModelToGameInput
    {
        public static GameInput Convert(BetModel betModel, decimal walletBalance)
        {
            var gameInput = new GameInput();
            gameInput.WalletBalance = walletBalance;
            gameInput.Difficulty = betModel.Difficulty;
            gameInput.Currency = TransformCurrency(betModel.Currency);
            gameInput.BetAmount = betModel.BetAmount;

            return gameInput;
        }

        private static CryptoGambling.Core.Games.Enums.Currencies TransformCurrency(CryptoGambling.Data.Funds.Currency currency)
        {
            switch (currency)
            {
                case Currency.Btc:
                    return CryptoGambling.Core.Games.Enums.Currencies.Btc;
                case Currency.Ltc:
                    return CryptoGambling.Core.Games.Enums.Currencies.Ltc;

                case Currency.Doge:
                    return CryptoGambling.Core.Games.Enums.Currencies.Doge;

                default:
                    return CryptoGambling.Core.Games.Enums.Currencies.Btc;

            }
        }
    }
}
