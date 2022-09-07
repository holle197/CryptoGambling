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
    public class MainWallet
    {
        private readonly CryptoWallet _wallet;
        public MainWallet(WalletModel walletModel)
        {
            _wallet = new CryptoWallet(walletModel.WalletSecret, walletModel.Network, NBitcoin.ScriptPubKeyType.Legacy);
        }

        public async Task<WithdrawalResult> Withdraw(WithdrawalModel withdrawalModel)
        {
            if (withdrawalModel.DestinationAddress is null) return new WithdrawalResult();

            var txHash = await _wallet.PushTxAsync(withdrawalModel.DestinationAddress, withdrawalModel.Amount,
                                                      WalletInfo.GetTransactionFee(_wallet.GetNetwork()));
            return new WithdrawalResult()
            {
                Amount = withdrawalModel.Amount,
                TxHash = txHash
            };
        }
    }
}
