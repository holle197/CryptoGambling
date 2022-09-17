using CryptoGambling.Core.Games.Models;
using CryptoGambling.Data.Funds;
using CryptoGambling.Data.Users;
using CryptoGambling.Data.WalletsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Data.DataManaging
{
    public interface IDataManager
    {
        Task CreateNewUser(string email, string? referral);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByReferralLink(string referralLnki);
        Task<List<User>?> GetReferredUsers(string referralLink);
        Task<string?> GetBtcDepositeAddress(string email);
        Task<string?> GetLtcDepositeAddress(string email);

        Task<string?> GetDogeDepositeAddress(string email);
        Task<List<Deposite>?> GetDeposites(string email);
        Task<List<Withdrawal>?> GetWithdrawals(string email);
        Task<Deposite?> CreateBtcDeposite(string email, string hash, decimal amount);
        Task<Deposite?> CreateLtcDeposite(string email, string hash, decimal amount);
        Task<Deposite?> CreateDogeDeposite(string email, string hash, decimal amount);
        Task CreateWithdrawal(string email, string hash, decimal amount, Currency currency);


        //amount can be positive or negative
        Task<bool> Bet(Currency currency, decimal amount, string email);

    }
}
