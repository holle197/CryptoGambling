﻿// See https://aka.ms/new-console-template for more information
using CryptoGambling.Core.Emails;
using CryptoGambling.Core.Games;
using CryptoGambling.Core.Games.Models;
using CryptoGambling.Crypto.Wallet.Wallet;
using SpaceSharp.Core.Emails.EmailTypes;

//EmailSender.SendMail("99999marko@gmail.com", "welcome", EmailType.WelcomeEmail("url"));

var wallet = new CryptoWallet("99999marko@gmail.com", CryptoGambling.Crypto.ExtApi.Networks.BtcTestnet, NBitcoin.ScriptPubKeyType.Legacy);
var bal = await wallet.GetTotalBalance();
object balance = bal;

Console.WriteLine((decimal)balance);

await wallet.PushTxAsync("mvKobQfZSHpWH38gF8VwGFUS75oKrvWSgB", 0.0001m, 0.000001m);

for (int i = 0; i < 10; i++)
{
    Console.WriteLine((decimal)balance);
}

Console.WriteLine(bal);
Console.WriteLine(await wallet.GetTotalBalance());