using CryptoGambling.Crypto.Wallet.Wallet;
using CryptoGambling.Data.CashRegisters;
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

        public async Task<string?> GetBtcDepositeAddress(string email)
        {
            var user = await GetUserByEmail(email);
            if (user is not null)
            {
                if (user.Wallet is not null)
                {
                    return user.Wallet.BtcAddress;
                }
                return null;
            }
            return null;
        }
        public async Task<string?> GetLtcDepositeAddress(string email)
        {
            var user = await GetUserByEmail(email);
            if (user is not null)
            {
                if (user.Wallet is not null)
                {
                    return user.Wallet.LtcAddress;
                }
                return null;
            }
            return null;
        }

        public async Task<string?> GetDogeDepositeAddress(string email)
        {
            var user = await GetUserByEmail(email);
            if (user is not null)
            {
                if (user.Wallet is not null)
                {
                    return user.Wallet.DogeAddress;
                }
                return null;
            }
            return null;
        }

        public async Task<Deposite?> CreateBtcDeposite(string email, string hash, decimal amount)
        {
            var user = await _data.Users.Include(user => user.Deposites).Include(user => user.Wallet)
                .Where(user => user.Email == email).FirstOrDefaultAsync();

            if (user is not null)
            {
                var deposites = user.Deposites;
                var wallet = user.Wallet;
                if (deposites is not null)
                {
                    var deposite = CreateNewDeposite(hash, amount, Currency.Btc);
                    deposites.Add(deposite);
                    UpdateBalance(wallet, amount, Currency.Btc);
                    await _data.SaveChangesAsync();
                    return deposite;
                }
                else
                {
                    var deposite = CreateNewDeposite(hash, amount, Currency.Btc);
                    user.Deposites = new();
                    user.Deposites.Add(deposite);
                    UpdateBalance(wallet, amount, Currency.Btc);
                    await _data.SaveChangesAsync();
                    return deposite;
                }
            }

            return null;
        }

        public async Task<Deposite?> CreateLtcDeposite(string email, string hash, decimal amount)
        {
            var user = await _data.Users.Include(user => user.Deposites).Include(user => user.Wallet)
                .Where(user => user.Email == email).FirstOrDefaultAsync();

            if (user is not null)
            {
                var deposites = user.Deposites;
                var wallet = user.Wallet;
                if (deposites is not null)
                {
                    var deposite = CreateNewDeposite(hash, amount, Currency.Ltc);
                    deposites.Add(deposite);
                    UpdateBalance(wallet, amount, Currency.Ltc);
                    await _data.SaveChangesAsync();
                    return deposite;
                }
                else
                {
                    var deposite = CreateNewDeposite(hash, amount, Currency.Ltc);
                    user.Deposites = new();
                    user.Deposites.Add(deposite);
                    UpdateBalance(wallet, amount, Currency.Ltc);
                    await _data.SaveChangesAsync();
                    return deposite;
                }
            }

            return null;
        }

        public async Task<Deposite?> CreateDogeDeposite(string email, string hash, decimal amount)
        {
            var user = await _data.Users.Include(user => user.Deposites).Include(user => user.Wallet)
                .Where(user => user.Email == email).FirstOrDefaultAsync();

            if (user is not null)
            {
                var deposites = user.Deposites;
                var wallet = user.Wallet;
                if (deposites is not null)
                {
                    var deposite = CreateNewDeposite(hash, amount, Currency.Doge);
                    deposites.Add(deposite);
                    UpdateBalance(wallet, amount, Currency.Doge);
                    await _data.SaveChangesAsync();
                    return deposite;
                }
                else
                {
                    var deposite = CreateNewDeposite(hash, amount, Currency.Doge);
                    user.Deposites = new();
                    user.Deposites.Add(deposite);
                    UpdateBalance(wallet, amount, Currency.Doge);
                    await _data.SaveChangesAsync();
                    return deposite;
                }
            }

            return null;
        }


        public async Task CreateWithdrawal(string email, string hash, decimal amount, Currency currency)
        {
            var user = await GetUserByEmail(email);
            if (user is not null)
            {
                Withdrawal withdrawal = new Withdrawal();
                withdrawal.Hash = hash;
                withdrawal.Amount = amount;
                withdrawal.Currency = Currency.Btc;
                var withdrawals = user.Withdrawals;
                if (withdrawals is not null)
                {

                    user?.Withdrawals?.Add(withdrawal);
                    await _data.SaveChangesAsync();
                    return;
                }
                user.Withdrawals = new();
                user.Withdrawals.Add(withdrawal);
                await _data.SaveChangesAsync();
            }
        }

        public async Task<List<Deposite>?> GetDeposites(string email)
        {
            var user = await GetUserByEmail(email);

            return user?.Deposites?.ToList();
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
            return await _data.Users.Include(user => user.Wallet).Where(user => user.ReferralLink == referralLnki).FirstOrDefaultAsync();
        }



        public async Task<List<User>?> GetReferredUsers(string referralLink)
        {
            return await _data.Users.Include(user => user.Wallet).Where(user => user.ReferredBy == referralLink).ToListAsync();
        }

        public async Task<bool> Bet(Currency currency, decimal amount, string email)
        {
            var user = await _data.Users.Include(user => user.Wallet).Where(user => user.Email == email).FirstOrDefaultAsync();
            if (user is not null)
            {
                UpdateBalance(user.Wallet, amount, currency);
                //if user is refered and lose bet
                if (user.ReferredBy is not null && amount < 0)
                {
                    var earnedBalance = (amount / 4) * -1;
                    var sharedBalance = (amount / 4) * -1;
                    var reffBalance = (amount / 2) * -1;
                    UpdateReffBalance(user.Wallet, reffBalance, currency);
                    await UpdateCashRegister(currency, earnedBalance, sharedBalance);

                }
                UpdateBalance(user.Wallet, amount, currency);
                if (amount < 0)
                {
                    var sharedBalance = (amount / 2) * -1;
                    var earnedBalance = (amount / 2) * -1;
                    await UpdateCashRegister(currency, earnedBalance, sharedBalance);
                }
                else
                {
                    await UpdateCashRegister(currency, amount, 0m);
                }
                await _data.SaveChangesAsync();
                return true;
            }
            return false;
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


        private static Deposite CreateNewDeposite(string hash, decimal amount, Currency currency)
        {
            var deposite = new Deposite();
            deposite.Hash = hash;
            deposite.Amount = amount;
            deposite.Curreny = currency;

            return deposite;
        }

        // amount can be both,positive or negative
        private static void UpdateBalance(Wallets wallet, decimal amount, Currency currency)
        {
            switch (currency)
            {
                case Currency.Btc:
                    wallet.BtcBalance += amount;
                    break;
                case Currency.Ltc:
                    wallet.LtcBalance += amount;
                    break;
                case Currency.Doge:
                    wallet.DogeBalance += amount;
                    break;
            }
        }


        private static void UpdateReffBalance(Wallets wallet, decimal amount, Currency currency)
        {
            switch (currency)
            {
                case Currency.Btc:
                    wallet.BtcReferredBalance += amount;
                    break;
                case Currency.Ltc:
                    wallet.LtcReferredBalance += amount;
                    break;
                case Currency.Doge:
                    wallet.DogeReferredBalance += amount;
                    break;
            }
        }

        private async Task UpdateCashRegister(Currency currency, decimal earnedBalance, decimal sharedBalance)
        {
            var register = await _data.CashRegister.Where(x => x.Id == 1).FirstOrDefaultAsync();
            if (register is not null)
            {
                switch (currency)
                {
                    case Currency.Btc:
                        register.BtcEarnedBalance += earnedBalance;
                        register.BtcSharedBalance += sharedBalance;
                        break;
                    case Currency.Ltc:
                        register.LtcEarnedBalance = earnedBalance;
                        register.LtcSharedBalance = sharedBalance;
                        break;
                    case Currency.Doge:
                        register.DogeEarnedBalance = earnedBalance;
                        register.DogeSharedBalance = sharedBalance;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
