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
        Task CreateNewUser(string email);
        Task<User?> GetUserByEmail(string email);
        Task<User?> GetUserByReferralLink(string referralLnki);
        Task<List<User>?> GetReferredUsers(string referralLink);
        Task<List<Deposite>?> GetDeposites(string email);
        Task<List<Withdrawal>?> GetWithdrawals(string email);
        Task AddDeposite(string email, string hash, decimal amount);

    }
}
