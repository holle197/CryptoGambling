
using CryptoGambling.Core.Funds.Models;
using CryptoGambling.Core.Funds.Wallets.WalletsInfo;
using CryptoGambling.Crypto.Wallet.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Funds.Wallets
{
    public class UserWallet
    {

        private readonly CryptoWallet _wallet;
        public UserWallet(WalletModel walletModel)
        {
            _wallet = new CryptoWallet(walletModel.WalletSecret, walletModel.Network, NBitcoin.ScriptPubKeyType.Legacy);
        }

        public async Task<DepositeResult> TransferDepositsToMainWallet()
        {
            var balance = await _wallet.GetTotalBalance();
            var network = _wallet.GetNetwork();
            var fee = WalletInfo.GetTransactionFee(network);
            var minDepositeAmount = WalletInfo.GetMinDepositeAmount(network);
            if (balance >= minDepositeAmount)
            {
                var depositeResult = new DepositeResult();
                var transfetToMainAddr = WalletInfo.GetMainAdddress(network);
                decimal amount = balance - fee;
                var transactionHash = await _wallet.PushTxAsync(transfetToMainAddr, amount, fee);

                depositeResult.Balance = amount;
                depositeResult.TxHash = transactionHash;
                return depositeResult;
            }


            return new DepositeResult();
        }
    }
}
