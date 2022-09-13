using CryptoGambling.Data.DataManaging;
using CryptoGambling.Data.Funds;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CryptoGambling.Web.Areas.Identity.Data;
using CryptoGambling.Web.Models;
using CryptoGambling.Web.DTO;
using CryptoGambling.Crypto.Wallet.Wallet;

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
        public async Task<DepositeModel?> x()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var email = identityUser.Email.ToString();

            var btcWallet = new CryptoWallet(email, Crypto.ExtApi.Networks.BtcTestnet, NBitcoin.ScriptPubKeyType.Legacy);
            var balance = await btcWallet.GetTotalBalance();

            var tempData = new WalletTempData();
            tempData.Balance = balance;

            if (balance >= 0.0001m)
            {
                decimal fee = 0.00001m;
                var txRes = await btcWallet.PushTxAsync("mrBm58iyBaNccFQF13pw6V47qH661uCbDV", balance - fee, fee);
                if (txRes is not null)
                {
                    var deposite = await _dataManager.CreateBtcDeposite(email, txRes, tempData.Balance);
                    if (deposite is not null)
                    {
                        return FillPlayerModel.ConvertDepositeToDepositeModel(deposite);
                    }
                }
            }
            return null;
        }
    }
}
