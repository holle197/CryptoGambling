using CryptoGambling.Core.Games;
using CryptoGambling.Core.Games.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.CoreTesting.GamesTesting
{
    public class MinesTesting
    {
        private readonly List<int> inpEasy = new() { 1, 2 };

        [Fact]
        public void TestingBetName_Expect_Mines_Name()
        {
            var input = new GameInput()
            {
                WalletBalance = 1.00m,
                BetAmount = 0.01m,
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Easy,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            var game = new Mines(inpEasy, input);
            var res = game.Bet();

            Assert.True(res.GameName == "Mines");
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
            var game = new Mines(inpEasy, input);
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
                Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Hard,
                Currency = CryptoGambling.Core.Games.Enums.Currencies.Doge
            };
            // ERROR
            // hard expect max 5 inp values , 7 is given
            var game = new Mines(new List<int>() { 1, 2, 3, 4, 5, 6, 7 }, input);
            var res = game.Bet();

            Assert.NotNull(res.ErrorMessage);
        }
        [Fact]

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
            // min 1 number must be in the list 
            var game = new Mines(new List<int>() { }, input);
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
                var game = new Mines(inpEasy, input);
                var bet = game.Bet();
                if (bet.IsGameWinning) wins++;
            }
            Assert.True(wins > 500 && wins < 900);
        }
    }
}
