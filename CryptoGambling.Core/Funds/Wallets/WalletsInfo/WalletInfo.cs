using CryptoGambling.Crypto.ExtApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Funds.Wallets.WalletsInfo
{
    internal static class WalletInfo
    {
        public static decimal GetMinDepositeAmount(Networks network)
        {
            return network switch
            {
                Networks.BtcMainnet => 0.00015m,
                Networks.BtcTestnet => 0.00015m,

                Networks.LtcMainnet => 0.01m,
                Networks.LtcTestnet => 0.01m,

                Networks.DogeMainnet => 5m,
                Networks.DogeTestnet => 5m,
                _ => 0.00007m,
            };
        }
        public static decimal GetTransactionFee(Networks network)
        {
            return network switch
            {
                Networks.BtcMainnet => 0.00007m,
                Networks.BtcTestnet => 0.00007m,

                Networks.LtcMainnet => 0.001m,
                Networks.LtcTestnet => 0.001m,

                Networks.DogeMainnet => 1m,
                Networks.DogeTestnet => 1m,
                _ => 0.00007m,
            };
        }

        public static string GetMainAdddress(Networks network)
        {
            return network switch
            {
                Networks.BtcMainnet => "",
                Networks.BtcTestnet => "",

                Networks.LtcMainnet => "",
                Networks.LtcTestnet => "",

                Networks.DogeMainnet => "",
                Networks.DogeTestnet => "",
                _ => ""
            };
        }
    }
}
