using CryptoGambling.Data.DataManaging;
using CryptoGambling.Data.Funds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CryptoGambling.Web.Areas.Identity.Data;
using CryptoGambling.Web.Models;
using CryptoGambling.Web.DTO;
using CryptoGambling.Crypto.Wallet.Wallet;
using CryptoGambling.Core.Games.Models;
using CryptoGambling.Core.Games;

namespace CryptoGambling.Web.Controllers
{
    public class StairsController : Controller
    {
        private readonly IDataManager _dataManager;
        private readonly UserManager<UserIdentity> _userManager;


        public StairsController(IDataManager dataManager, UserManager<UserIdentity> userManager)
        {
            _dataManager = dataManager;
            _userManager = userManager;

        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            PlayerModel playerModel = new();
            var identityUser = await _userManager.GetUserAsync(User);
            var email = identityUser.Email.ToString();

            var user = await _dataManager.GetUserByEmail(email);

            if (user is not null)
            {
                FillPlayerModel.Fill(playerModel, user);
            }

            return View(playerModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<GameOutput?> Bet(BetModel betModel)
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var email = identityUser.Email.ToString();
            if (betModel is not null)
            {
                var balance = await _dataManager.GetUserBalance(email, betModel.Currency);
                var sharedBalance = await _dataManager.GetSharedBalance(betModel.Currency);
                var gameInput = BetModelToGameInput.Convert(betModel, balance);
                var game = new SpaceExplorer(betModel.IntLuckyField, gameInput);
                var res = game.Bet();
                // force to lose
                if (res.IsGameWinning == true && res.Profit > sharedBalance)
                {
                    res.IsGameWinning = false;
                    res.Profit *= -1;
                    await _dataManager.Bet(betModel.Currency, res.Profit, email);
                    return res;
                }
                await _dataManager.Bet(betModel.Currency, res.Profit, email);
                return res;
            }
            return null;

        }


    }
}
