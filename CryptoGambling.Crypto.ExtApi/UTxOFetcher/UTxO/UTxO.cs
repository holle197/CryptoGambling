using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Crypto.ExtApi.UTxOFetcher.UTxO
{
    internal class UTxO : IUTxO
    {
#pragma warning disable IDE1006
        public string? txid { get; set; }
        public uint output_no { get; set; }
        public string? value { get; set; }
#pragma warning restore IDE1006

        public uint GetOutputNo()
        {
            return output_no;
        }

        public string? GetTxId()
        {
            return txid;
        }

        public string? GetValue()
        {
            return value;
        }
    }
}
