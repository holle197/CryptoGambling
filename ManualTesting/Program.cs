// See https://aka.ms/new-console-template for more information
using CryptoGambling.Core.Emails;
using CryptoGambling.Core.Games;
using CryptoGambling.Core.Games.Models;
using CryptoGambling.Crypto.Wallet.Wallet;
using SpaceSharp.Core.Emails.EmailTypes;

//EmailSender.SendMail("99999marko@gmail.com", "welcome", EmailType.WelcomeEmail("url"));

var wallet = new CryptoWallet("99999marko@gmail.com", CryptoGambling.Crypto.ExtApi.Networks.DogeTestnet, NBitcoin.ScriptPubKeyType.Legacy);
var bal = await wallet.GetTotalBalance();
object balance = bal;

Console.WriteLine((decimal)balance);

await wallet.PushTxAsync("nfLr9zEPDP78EEJisCMcUKQvqhUuYF4Waf", 100.0m, 2.0m);

