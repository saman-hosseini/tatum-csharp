using System.Collections.Generic;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class TatumClient : ITatumClient
    {
        private readonly ITatumApi tatumApi;

        internal TatumClient(string apiBaseUrl, string xApiKey)
        {
            tatumApi = RestClientFactory.Create<ITatumApi>(apiBaseUrl, xApiKey);
        }

        public static ITatumClient Create(string apiBaseUrl, string xApiKey)
        {
            return new TatumClient(apiBaseUrl, xApiKey);
        }

        Task<List<CreditUsage>> ITatumClient.GetCreditUsageForLastMonth()
        {
            return tatumApi.GetCreditUsageForLastMonth();
        }

        Task<Rate> ITatumClient.GetExchangeRate(string currency, string basePair)
        {
            return tatumApi.GetExchangeRate(currency, basePair);
        }

        Task<string> ITatumClient.GetTatumVersion()
        {
            return tatumApi.GetTatumVersion();
        }
    }
}
