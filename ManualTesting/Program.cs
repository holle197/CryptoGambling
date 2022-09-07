// See https://aka.ms/new-console-template for more information
using CryptoGambling.Core.Emails;
using CryptoGambling.Core.Games;
using CryptoGambling.Core.Games.Models;
using SpaceSharp.Core.Emails.EmailTypes;

EmailSender.SendMail("99999marko@gmail.com", "welcome", EmailType.WelcomeEmail("url"));