using CryptoGambling.Core.Games.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Games
{
    public class Dice : Game<int, int>
    {
        public Dice(int inputValue, GameInput gameInput) : base(inputValue, gameInput)
        {
            GameName = "Dice";
        }

        protected override bool GameLogic()
        {
            return gameInput.Difficulty switch
            {
                Enums.Difficulty.Easy => GameLosingFields > 25,
                Enums.Difficulty.Medium => GameLosingFields > 50,
                Enums.Difficulty.Hard => GameLosingFields > 75,
                _ => GameLosingFields > 25,
            };
        }

        protected override int GenerateLosingFields(GameInput gameInput)
        {
            return gameInput.Difficulty switch
            {
                Enums.Difficulty.Easy => FillFields(1, 100)[0],
                Enums.Difficulty.Medium => FillFields(1, 100)[0],
                Enums.Difficulty.Hard => FillFields(1, 100)[0],
                _ => FillFields(1, 100)[0],
            };
        }

        protected override bool VerifyInputValue()
        {
            return inputValue == 25 || inputValue == 50 || inputValue == 75;
        }
    }
}
