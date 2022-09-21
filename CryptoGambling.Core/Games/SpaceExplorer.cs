using CryptoGambling.Core.Games.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Games
{
    public class SpaceExplorer : Game<int, List<int>>
    {
        public SpaceExplorer(int inputValue, GameInput gameInput) : base(inputValue, gameInput)
        {
            GameName = "SpaceExplorer";
        }
        protected override bool GameLogic()
        {
            return !GameLosingFields.Contains(inputValue);
        }

        protected override List<int> GenerateLosingFields(GameInput gameInput)
        {
            return gameInput.Difficulty switch
            {
                Enums.Difficulty.Easy => FillFields(1, 4),
                Enums.Difficulty.Medium => FillFields(2, 4),
                Enums.Difficulty.Hard => FillFields(3, 4),
                _ => FillFields(1, 4),
            };
        }

        protected override bool VerifyInputValue()
        {
            return inputValue >= 1 && inputValue <= 4;
        }
    }
}
