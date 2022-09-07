using CryptoGambling.Core.Games.Enums;
using CryptoGambling.Core.Games.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Games
{
    public class Mines : Game<List<int>, List<int>>
    {
        public Mines(List<int> inputValue, GameInput gameInput) : base(inputValue, gameInput)
        {
            GameName = "Mines";
        }

        protected override bool GameLogic()
        {
            return !inputValue.Any(x => GameLosingFields.Any(y => y == x));
        }

        protected override List<int> GenerateLosingFields(GameInput gameInput)
        {
            return gameInput.Difficulty switch
            {
                Enums.Difficulty.Easy => FillFields(5, 20),
                Enums.Difficulty.Medium => FillFields(10, 20),
                Enums.Difficulty.Hard => FillFields(15, 20),
                _ => FillFields(5, 20),
            };
        }

        // ADD CHECK IF LIST HAVE UNIQUE NUMBERS    
        protected override bool VerifyInputValue()
        {
            return gameInput.Difficulty switch
            {
                Enums.Difficulty.Easy => inputValue.Count >= 1 && inputValue.Count <= 15,
                Enums.Difficulty.Medium => inputValue.Count >= 1 && inputValue.Count <= 10,
                Enums.Difficulty.Hard => inputValue.Count >= 1 && inputValue.Count <= 5,
                _ => inputValue.Count >= 1 && inputValue.Count <= 15,
            };
        }

        // Mines have multiple fields and multiple int inputs (list<int>)
        // formula for generating quotes :  diff * inp.Count / 2
        protected override decimal GenerateQuote()
        {
            return gameInput.Difficulty switch
            {
                Difficulty.Easy => 1.24m * inputValue.Count / 2,
                Difficulty.Medium => 1.98m * inputValue.Count / 2,
                Difficulty.Hard => 3.96m * inputValue.Count / 2,
                _ => 1.32m,
            };
        }
    }
}
