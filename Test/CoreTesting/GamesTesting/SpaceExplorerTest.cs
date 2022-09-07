using CryptoGambling.Core.Games;
using CryptoGambling.Core.Games.Models;
using Xunit;

namespace Test.CoreTesting.GamesTesting
{
    public class SpaceExplorerTest
    {

        // ORIGINAL BASE METHOD THAT NOT OVERLOADING OR OVERRIDING IN ANY CHILD CLASS
        // Only 1 test is enough, ValidateBalance() is parent method in class Game
        //  that all games inherit from this class
        [Fact]
        public void TestingBet_Expect_Insufficient_Funds()
        {
            var input = new GameInput()
            {
                WalletBalance = 1.00m,
                // ERROR
                BetAmount = 2.01m,
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Easy,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            var game = new SpaceExplorer(1, input);
            var res = game.Bet();

            Assert.NotNull(res.ErrorMessage);
        }

        // ORIGINAL BASE METHOD THAT NOT OVERLOADING OR OVERRIDING IN ANY CHILD CLASS
        [Fact]
        public void TestingBet_Expect_Insufficient_Amount()
        {
            var input = new GameInput()
            {
                WalletBalance = 1.00m,
                // ERROR
                BetAmount = 2.00m,
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Easy,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            var game = new SpaceExplorer(1, input);
            var res = game.Bet();

            Assert.NotNull(res.ErrorMessage);
        }

        [Fact]
        public void TestingBetName_Expect_SpaceExplorer_Name()
        {
            var input = new GameInput()
            {
                WalletBalance = 1.00m,
                BetAmount = 0.01m,
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Easy,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            var game = new SpaceExplorer(1, input);
            var res = game.Bet();

            Assert.True(res.GameName == "SpaceExplorer");
        }
        [Fact]

        public void TestingBet_Expect_Regular_Game_Output()
        {
            var input = new GameInput()
            {
                WalletBalance = 1.00m,
                BetAmount = 0.01m,
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Easy,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            var game = new SpaceExplorer(1, input);
            var res = game.Bet();

            // If error msg is null,everything is fine
            Assert.Null(res.ErrorMessage);
        }



        [Fact]
        // Expecting ERROR, min number for bet field is 1 -> 0 is given
        public void TestingBet_Expect_Error_Game_Params1()
        {
            var input = new GameInput()
            {
                WalletBalance = 1.00m,
                BetAmount = 0.000000001m,
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Easy,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            // ERROR
            var game = new SpaceExplorer(0, input);
            var res = game.Bet();

            Assert.NotNull(res.ErrorMessage);
        }
        [Fact]
        // Expecting ERROR, max number for bet field is 4 -> 5 is given
        public void TestingBet_Expect_Error_Game_Params2()
        {
            var input = new GameInput()
            {
                WalletBalance = 1.00m,
                BetAmount = 0.000000001m,
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Easy,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            // ERROR
            var game = new SpaceExplorer(5, input);
            var res = game.Bet();

            Assert.NotNull(res.ErrorMessage);
        }

        [Fact]
        public void TestingBet_Probability_Fair_Easy_Expect_75_Percent_Wins()
        {
            var input = new GameInput()
            {
                WalletBalance = 10.00m,
                BetAmount = 1m,
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Easy,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            int wins = 0;

            for (int i = 0; i < 1000; i++)
            {
                var game = new SpaceExplorer(1, input);
                var bet = game.Bet();
                if (bet.IsGameWinning) wins++;
            }
            Assert.True(wins > 500 && wins < 900);
        }


        [Fact]
        public void TestingBet_Probability_Fair_Medium_Expect_50_Percent_Wins()
        {
            var input = new GameInput()
            {
                WalletBalance = 10.00m,
                BetAmount = 1m,
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Medium,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            int wins = 0;

            for (int i = 0; i < 1000; i++)
            {
                var game = new SpaceExplorer(1, input);
                var bet = game.Bet();
                if (bet.IsGameWinning) wins++;
            }
            Assert.True(wins > 300 && wins < 700);
        }


        [Fact]
        public void TestingBet_Probability_Fair_Hard_Expect_25_Percent_Wins()
        {
            var input = new GameInput()
            {
                WalletBalance = 10.00m,
                BetAmount = 1m,
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Hard,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            int wins = 0;

            for (int i = 0; i < 1000; i++)
            {
                var game = new SpaceExplorer(1, input);
                var bet = game.Bet();
                if (bet.IsGameWinning) wins++;
            }
            Assert.True(wins > 100 && wins < 500);
        }
    }
}
