using CryptoGambling.Crypto.ExtApi.TxBroadcast.TransactionResult;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Crypto.ExtApi.TxBroadcast
{
    public class TxPusher
    {
        public static async Task<string?> BroadcastRawTxAsync(Networks networkUrl, string txHex)
        {
            try
            {
                if (string.IsNullOrEmpty(txHex)) return null;

                var client = new RestClient(TxBroadcastUrls.UrlFor(networkUrl))
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(new { tx_hex = txHex });
                var response = await client.ExecuteAsync(request);
                var txHash = System.Text.Json.JsonSerializer.Deserialize<TxData>(response.Content);

                return txHash?.data?.txid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
