using CryptoGambling.Crypto.Wallet.Wallet;
using CryptoGambling.Data.DataContext;
using CryptoGambling.Data.Funds;
using CryptoGambling.Data.Users;
using CryptoGambling.Data.WalletsData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Data.DataManaging
{
    public class DataManager : IDataManager
    {
        private readonly DataUserContext _data;

        public DataManager(DataUserContext _data)
        {
            this._data = _data;
        }
        public async Task CreateNewUser(string email, string? referral)
        {
            User user = new User();
            user.Email = email;
            Wallets wallets = new();

            wallets.BtcAddress = GetBtcAddress(email);
            wallets.LtcAddress = GetLtcAddress(email);
            wallets.DogeAddress = GetDogeAddress(email);

            user.Wallet = wallets;

            user.Deposites = new();
            user.Withdrawals = new();

            if (referral is not null)
            {
                var refExist = GetUserByReferralLink(referral);
                if (refExist is not null)
                {
                    user.ReferredBy = referral;
                    _data.Users.Add(user);
                    await _data.SaveChangesAsync();
                    return;
                }
            }
            _data.Users.Add(user);
            await _data.SaveChangesAsync();

        }

        public async Task AddDeposite(string email, string hash, decimal amount)
        {
            var user = await GetUserByEmail(email);
            if (user is not null)
            {
                Deposite d = new Deposite();
                d.DepositeHash = hash;
                d.Amount = amount;

                var deposites = user.Deposites;
                if (deposites is not null)
                {

                    user?.Deposites?.Add(d);
                    await _data.SaveChangesAsync();
                    return;
                }
                user.Deposites = new();
                user.Deposites.Add(d);
                await _data.SaveChangesAsync();
            }
        }

        public async Task<List<Deposite>?> GetDeposites(string email)
        {
            var user = await GetUserByEmail(email);
            if (user is not null)
            {
                var deposites = user?.Deposites?.ToList();
                if (deposites is not null)
                {
                    return deposites;
                }
                return null;
            }
            return null;
        }
        public async Task<List<Withdrawal>?> GetWithdrawals(string email)
        {
            var user = await GetUserByEmail(email);
            if (user is not null)
            {
                var withdrawals = user?.Withdrawals?.ToList();
                if (withdrawals is not null)
                {
                    return withdrawals;
                }
                return null;
            }
            return null;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _data.Users.Include(user => user.Wallet).Include(user => user.Deposites)
                .Include(user => user.Withdrawals).Where(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByReferralLink(string referralLnki)
        {
            return await _data.Users.Where(user => user.ReferralLink == referralLnki).FirstOrDefaultAsync();
        }




        public async Task<List<User>?> GetReferredUsers(string referralLink)
        {
            return await _data.Users.Where(user => user.ReferredBy == referralLink).ToListAsync();
        }





        private string GetBtcAddress(string email)
        {
            CryptoWallet cw = new CryptoWallet(email, Crypto.ExtApi.Networks.BtcTestnet, NBitcoin.ScriptPubKeyType.Legacy);
            return cw.GetAddress();
        }
        private string GetLtcAddress(string email)
        {
            CryptoWallet cw = new CryptoWallet(email, Crypto.ExtApi.Networks.LtcTestnet, NBitcoin.ScriptPubKeyType.Legacy);
            return cw.GetAddress();
        }
        private string GetDogeAddress(string email)
        {
            CryptoWallet cw = new CryptoWallet(email, Crypto.ExtApi.Networks.DogeTestnet, NBitcoin.ScriptPubKeyType.Legacy);
            return cw.GetAddress();
        }
    }
}
