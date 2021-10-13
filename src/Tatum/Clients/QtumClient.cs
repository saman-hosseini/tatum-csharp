using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class QtumClient: BaseClient, IQtumClient
    {
        private readonly IQtumApi qtumApi;

        internal QtumClient()
        {
        }

        internal QtumClient(string apiBaseUrl, string xApiKey)
        {
            qtumApi = RestClientFactory.Create<IQtumApi>(apiBaseUrl, xApiKey);
        }

        public static IQtumClient Create(string apiBaseUrl, string xApiKey)
        {
            return new QtumClient(apiBaseUrl, xApiKey);
        }

        Task<QtumBalance> IQtumClient.GetBalance(string address)
        {
            return qtumApi.GetBalance(address);
        }
    }
}
