using CryptoGambling.Core.Games.Enums;
using CryptoGambling.Core.Games.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Games
{
    public abstract class Game<InputValue, GameFields>
    {
        protected readonly InputValue inputValue;
        protected GameFields GameLosingFields;
        protected readonly GameInput gameInput;
        protected string? GameName;
        protected GameOutput gameOutput = new();
        protected readonly Random random = new();
        public Game(InputValue inputValue, GameInput gameInput)
        {
            this.inputValue = inputValue;
            this.gameInput = gameInput;
            this.GameLosingFields = GenerateLosingFields(gameInput);
        }

        public GameOutput Bet()
        {
            if (!ValidateBetBalance()) return new GameOutput() { ErrorMessage = "Insufficient Funds." };
            if (!VerifyInputValue()) return new GameOutput { ErrorMessage = "Invalid Game Params." };
            if (!ValidAmount()) return new GameOutput { ErrorMessage = $"Insufficient Amount. Amount must be greather or equal to {GetBetMinAmount()} {gameInput.Currency}" };
            var quote = GenerateQuote();
            var gameResult = GameLogic();
            return GenerateGameOutput(gameResult, quote, GenerateProfit(gameResult, quote));
        }
        protected abstract bool GameLogic();
        protected abstract bool VerifyInputValue();
        protected abstract GameFields GenerateLosingFields(GameInput gameInput);
        private bool ValidateBetBalance()
        {
            return gameInput.WalletBalance >= gameInput.BetAmount;
        }

        private bool ValidAmount()
        {
            return gameInput.Currency switch
            {
                Currencies.Btc => gameInput.BetAmount >= 0.00001m,
                Currencies.Ltc => gameInput.BetAmount >= 0.00001m,
                Currencies.Doge => gameInput.BetAmount >= 1m,
                _ => gameInput.BetAmount >= 0.00001m
            };
        }

        // fill fields with unique random number
        // prepare for game logick
        protected List<int> FillFields(int numOfLosseFields, int range)
        {
            var res = new List<int>();
            while (true)
            {
                var rnd = random.Next(1, range + 1);
                if (!res.Contains(rnd)) res.Add(rnd);
                if (res.Count == numOfLosseFields) break;
            }
            return res;
        }

        // override method in Mines
        protected virtual decimal GenerateQuote()
        {
            return gameInput.Difficulty switch
            {
                Difficulty.Easy => 1.24m,
                Difficulty.Medium => 1.98m,
                Difficulty.Hard => 3.96m,
                _ => 1.24m,
            };
        }
        // Generate profit based on difficulty
        // if game is lost return negative amount
        private decimal GenerateProfit(bool isGameWinning, decimal quote)
        {
            if (isGameWinning)
            {
                return gameInput.BetAmount * quote - gameInput.BetAmount;
            }
            return gameInput.BetAmount * -1m;
        }
        private GameOutput GenerateGameOutput(bool isGameWinning, decimal quote, decimal profit)
        {
            return new GameOutput()
            {
                Currency = gameInput.Currency,
                IsGameWinning = isGameWinning,
                GameName = GameName,
                Quote = quote,
                Amount = gameInput.BetAmount,
                Profit = profit,
            };
        }
        private decimal GetBetMinAmount()
        {
            return gameInput.Currency switch
            {
                Currencies.Btc => 0.00001m,
                Currencies.Ltc => 0.00001m,
                Currencies.Doge => 1m,
                _ => 0.00001m
            };
        }
    }
}
