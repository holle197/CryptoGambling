using CryptoGambling.Crypto.ExtApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Core.Funds.Models
{
    public class WalletModel
    {
        // Wallet Secret represent combinations of user's email and random seed
        // This is key for generating private key and must be the same all the time
        // otherwise wallet will not be the same.
        public string WalletSecret { get; set; } = "";
        public Networks Network { get; set; }
    }
}
