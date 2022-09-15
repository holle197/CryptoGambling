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

        public async Task<string?> GetBtcDepositeAddress()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var email = identityUser.Email.ToString();

            return await _dataManager.GetBtcDepositeAddress(email);
        }

        [Authorize]

        public async Task<string?> GetLtcDepositeAddress()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var email = identityUser.Email.ToString();

            return await _dataManager.GetLtcDepositeAddress(email);
        }

        [Authorize]

        public async Task<string?> GetDogeDepositeAddress()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var email = identityUser.Email.ToString();

            return await _dataManager.GetDogeDepositeAddress(email);
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
        public async Task<DepositeModel?> CheckBtcDeposite()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var email = identityUser.Email.ToString();

            var btcWallet = new CryptoWallet(email, Crypto.ExtApi.Networks.BtcTestnet, NBitcoin.ScriptPubKeyType.Legacy);
            var balance = await btcWallet.GetTotalBalance();

            if (balance >= 0.0001m)
            {
                decimal fee = 0.00001m;
                var txRes = await btcWallet.PushTxAsync("mrBm58iyBaNccFQF13pw6V47qH661uCbDV", balance - fee, fee);
                if (txRes is not null)
                {
                    var deposite = await _dataManager.CreateBtcDeposite(email, txRes, balance);
                    if (deposite is not null)
                    {
                        return FillPlayerModel.ConvertDepositeToDepositeModel(deposite);
                    }
                }
            }
            return null;
        }

        public async Task<DepositeModel?> CheckLtcDeposite()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var email = identityUser.Email.ToString();

            var ltcWallet = new CryptoWallet(email, Crypto.ExtApi.Networks.LtcTestnet, NBitcoin.ScriptPubKeyType.Legacy);
            var balance = await ltcWallet.GetTotalBalance();

            if (balance >= 0.001m)
            {
                decimal fee = 0.001m;
                var txRes = await ltcWallet.PushTxAsync("mrBm58iyBaNccFQF13pw6V47qH661uCbDV", balance - fee, fee);
                if (txRes is not null)
                {
                    var deposite = await _dataManager.CreateLtcDeposite(email, txRes, balance);
                    if (deposite is not null)
                    {
                        return FillPlayerModel.ConvertDepositeToDepositeModel(deposite);
                    }
                }
            }
            return null;
        }

        public async Task<DepositeModel?> CheckDogeDeposite()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var email = identityUser.Email.ToString();

            var dogeWallet = new CryptoWallet(email, Crypto.ExtApi.Networks.DogeTestnet, NBitcoin.ScriptPubKeyType.Legacy);
            var balance = await dogeWallet.GetTotalBalance();

            if (balance >= 10m)
            {
                decimal fee = 1m;
                var txRes = await dogeWallet.PushTxAsync("nery3MKYbwJNF7gR3tVa4jbh6HbyRB6anJ", balance - fee, fee);
                if (txRes is not null)
                {
                    var deposite = await _dataManager.CreateDogeDeposite(email, txRes, balance);
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
