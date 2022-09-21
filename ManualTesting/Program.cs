// See https://aka.ms/new-console-template for more information
using CryptoGambling.Core.Emails;
using CryptoGambling.Core.Games;
using CryptoGambling.Core.Games.Models;
using CryptoGambling.Crypto.Wallet.Wallet;
using SpaceSharp.Core.Emails.EmailTypes;

//EmailSender.SendMail("99999marko@gmail.com", "welcome", EmailType.WelcomeEmail("url"));
/*
var wallet = new CryptoWallet("99999marko@gmail.com", CryptoGambling.Crypto.ExtApi.Networks.DogeTestnet, NBitcoin.ScriptPubKeyType.Legacy);
var bal = await wallet.GetTotalBalance();
object balance = bal;

Console.WriteLine((decimal)balance);

await wallet.PushTxAsync("nfLr9zEPDP78EEJisCMcUKQvqhUuYF4Waf", 100.0m, 2.0m);*/
var inp = new GameInput();
inp.Currency = CryptoGambling.Core.Games.Enums.Currencies.Btc;
inp.WalletBalance = 1m;
inp.BetAmount = 0.1m;
inp.Difficulty = CryptoGambling.Core.Games.Enums.Difficulty.Hard;
var res = new List<bool>();
for (int i = 0; i < 100; i++)
{
    var g = new SpaceExplorer(1, inp);
    var gg = g.Bet();
    res.Add(gg.IsGameWinning);
}
foreach (var item in res)
{
    Console.WriteLine(item);
}