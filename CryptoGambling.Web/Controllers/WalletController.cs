using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CryptoGambling.Web.Areas.Identity.Data;
using CryptoGambling.Web.Models;
using CryptoGambling.Web.DTO;
using CryptoGambling.Data.DataManaging;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using CryptoGambling.Data.Funds;
using CryptoGambling.Crypto.Wallet.Wallet;

namespace CryptoGambling.Web.Controllers
{
    public class WalletController : Controller
    {
        private readonly IDataManager _dataManager;
        private readonly UserManager<UserIdentity> _userManager;

        public WalletController(IDataManager dataManager, UserManager<UserIdentity> userManager)
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
            return View();
        }


        [Authorize]

        public async Task<PlayerModel> GetBalances()
        {
            PlayerModel playerModel = new();
            var identityUser = await _userManager.GetUserAsync(User);
            var email = identityUser.Email.ToString();

            var user = await _dataManager.GetUserByEmail(email);
            if (user is not null)
            {
                FillPlayerModel.Fill(playerModel, user);
            }

            return playerModel;
        }

        [Authorize]
        // Check if user deposite, add deposite and balance to db 

        public async Task<Deposite?> ChechDeposite()
        {
            PlayerModel playerModel = new();
            var identityUser = await _userManager.GetUserAsync(User);
            var email = identityUser.Email.ToString();

            return null;


        }
    }
}
