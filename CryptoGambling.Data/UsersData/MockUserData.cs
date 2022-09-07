using CryptoGambling.Data.Users;
using CryptoGambling.Data.WalletsData;
using CryptoGambling.Crypto.Wallet.Wallet;
using CryptoGambling.Crypto.ExtApi;
using NBitcoin;

namespace CryptoGambling.Data.UserData
{
    public class MockUserData : IUserData
    {
        private readonly List<User> users = new()
        {
            new User()
            {
                Id = 1,
                Email = "vanjahocak@gmail.com",
                IsVerified = false,
                UserLoginId = "0x00",

                ReferredBy = null,
                ReferralLink = "1x111"
            }
        };
        private readonly List<Wallets> wallets = new()
        {
            new Wallets()
            {
                WalletEmail = "vanjahocak@gmail.com",
                BtcAddress = "0xbtc",
                LtcAddress = "oxltc",
                DogeAddress = "oxdoge",

                BtcBalance = 1m,
                LtcBalance = 1m,
                DogeBalance = 1m,

                BtcReferredBalance = 0m,
                LtcReferredBalance = 0m,
                DogeReferredBalance = 0m
            }
        };

        private const Networks btcNetwork = Networks.BtcTestnet;
        private const Networks ltcNetwork = Networks.LtcTestnet;
        private const Networks dogeNetwork = Networks.DogeTestnet;

        private const ScriptPubKeyType addrTypes = ScriptPubKeyType.Legacy;
        public async Task<bool> CreateNewUser(string email, string? referredBy)
        {
            if (email == null) return false;
            var userExist = users.Where(user => user.Email == email).FirstOrDefault();
            if (userExist != null) return false;

            int latestUserId = users.Max(user => user.Id);

            var btcWallet = CreateBtcWallet(email);
            var ltcWallet = CreateLtcWallet(email);
            var dogeWallet = CreateDogeWallet(email);

            users.Add(new User()
            {
                Id = ++latestUserId,
                Email = email,
                IsVerified = false,
                ReferredBy = referredBy,
                UserLoginId = GenerateRandomStr(),
                ReferralLink = GenerateRandomStr(),
            });
            wallets.Add(new Wallets()
            {
                WalletEmail = email,
                BtcAddress = btcWallet.GetAddress(),
                LtcAddress = ltcWallet.GetAddress(),
                DogeAddress = dogeWallet.GetAddress(),
            });
            await Task.Yield();
            return true;
        }

        public User? GetUserByEmail(string email)
        {
            return users.Where(user => user.Email == email).FirstOrDefault();
        }

        public User? GetUserByLoginId(string loginId)
        {
            return users.Where(user => user.UserLoginId == loginId).FirstOrDefault();
        }

        public User? GetUserByRefLink(string reffLink)
        {
            return users.Where(user => user.ReferralLink == reffLink).FirstOrDefault();
        }

        public Wallets? GetWallets(string email)
        {
            return wallets.Where(wallet => wallet.WalletEmail == email).FirstOrDefault();
        }
        public bool ReffLinkExist(string? reffLink)
        {
            if (reffLink == null) return false;
            var userExist = GetUserByRefLink(reffLink);
            return userExist != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email">Email of the user</param>
        /// <returns></returns>
        public Dictionary<string, decimal> GetRefBalance(string email)
        {
            var balances = new Dictionary<string, decimal>()
            {
                {"btc",0m},
                {"ltc",0m},
                {"doge",0m},
            };

            var referralWallets = GetWalletsOfReferrals(email);

            balances["btc"] = TotalBtcReffBalance(referralWallets);
            balances["ltc"] = TotalLtcReffBalance(referralWallets);
            balances["doge"] = TotalDogeReffBalance(referralWallets);

            return balances;
        }

        public bool ClaimReferralsBalances(string email)
        {
            var userWallet = GetWallets(email);
            var referralWallets = GetWalletsOfReferrals(email);
            var btcRefBal = TotalBtcReffBalance(referralWallets);
            var ltcRefBal = TotalLtcReffBalance(referralWallets);
            var dogeRefBal = TotalDogeReffBalance(referralWallets);


            if (btcRefBal == 0m && ltcRefBal == 0m && dogeRefBal == 0m || userWallet == null) return false;

            userWallet.BtcBalance += btcRefBal;
            userWallet.LtcBalance += ltcRefBal;
            userWallet.DogeBalance += dogeRefBal;
            MarkRefWalletsAsTaken(referralWallets);
            return true;
        }


        // email represent user secret for creating new wallets
        private static CryptoWallet CreateBtcWallet(string email)
        {
            return new CryptoWallet(email, btcNetwork, addrTypes);
        }
        private static CryptoWallet CreateLtcWallet(string email)
        {
            return new CryptoWallet(email, ltcNetwork, addrTypes);
        }
        private static CryptoWallet CreateDogeWallet(string email)
        {
            return new CryptoWallet(email, dogeNetwork, addrTypes);
        }

        private static string GenerateRandomStr()
        {
            return Guid.NewGuid().ToString();
        }
        private List<Wallets> GetWalletsOfReferrals(string email)
        {
            string? referrLink = users?.Where(user => user.Email == email).FirstOrDefault()?.ReferralLink;
            var allReferredUsers = users?.Where(user => user.ReferredBy == referrLink);
            var referredWallets = new List<Wallets>();

            if (allReferredUsers == null) return new List<Wallets>();

            foreach (var user in allReferredUsers)
            {
                var wallet = wallets.Where(u => u.WalletEmail == user.Email).FirstOrDefault();
                if (wallet != null && HaveRefBal(wallet)) referredWallets.Add(wallet);
            }

            return referredWallets;
        }

        private static decimal TotalBtcReffBalance(List<Wallets> wallets)
        {
            var res = 0m;
            foreach (var wallet in wallets)
            {
                res += wallet.BtcReferredBalance;
            }
            return res;
        }

        private static decimal TotalLtcReffBalance(List<Wallets> wallets)
        {
            var res = 0m;
            foreach (var wallet in wallets)
            {
                res += wallet.LtcReferredBalance;
            }
            return res;
        }
        private static decimal TotalDogeReffBalance(List<Wallets> wallets)
        {
            var res = 0m;
            foreach (var wallet in wallets)
            {
                res += wallet.DogeReferredBalance;
            }
            return res;
        }
        private static bool HaveRefBal(Wallets wallet)
        {
            return wallet.BtcReferredBalance > 0m || wallet.LtcReferredBalance > 0m || wallet.DogeReferredBalance > 0m;
        }


        private static void MarkRefWalletsAsTaken(List<Wallets> wallets)
        {
            foreach (var wallet in wallets)
            {
                wallet.BtcReferredBalance = 0m;
                wallet.LtcReferredBalance = 0m;
                wallet.DogeReferredBalance = 0m;
            }
        }


    }
}
