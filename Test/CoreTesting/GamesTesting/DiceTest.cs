using CryptoGambling.Core.Games;
using CryptoGambling.Core.Games.Models;
using Xunit;

namespace Test.CoreTesting.GamesTesting
{
    public class DiceTest
    {
        [Fact]
        public void TestingBet_Expect_Insufficient_Funds()
        {
            var input = new GameInput()
            {
                WalletBalance = 10.00m,
                BetAmount = 1.01m,
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Easy,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            var game = new Dice(1, input);
            var res = game.Bet();

            Assert.NotNull(res.ErrorMessage);
        }

    }
}
