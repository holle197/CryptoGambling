using CryptoGambling.Crypto.ExtApi.UTxOFetcher.UTxO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGambling.Crypto.ExtApi.UTxOFetcher
{
    public class UTxOFetcher
    {
        private readonly HttpClient _httpClient;

        public UTxOFetcher()
        {
            this._httpClient = new HttpClient();
        }

        public async Task<IUTxO[]?> FetchUTxOsAsync(Networks networkUrl, string address)
        {
            var apiCall = await _httpClient.GetAsync(UTxOUrls.UrlFor(networkUrl) + address);
            var asStr = await apiCall.Content.ReadAsStringAsync();
            var UTxOs = System.Text.Json.JsonSerializer.Deserialize<Data>(asStr);
            return UTxOs?.data?.txs;
        }
    }
}
