using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Crypto.ExtApi.UTxOFetcher.UTxO
{
    public interface IUTxO
    {
        public string? GetTxId();
        public string? GetValue();
        public uint GetOutputNo();
    }
}
