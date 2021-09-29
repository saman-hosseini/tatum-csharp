using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class PolygonClient : IPolygonClient
    {
        private readonly IPolygonApi oneApi;

        internal PolygonClient()
        {
        }

        internal PolygonClient(string apiBaseUrl, string xApiKey)
        {
            oneApi = RestClientFactory.Create<IPolygonApi>(apiBaseUrl, xApiKey);
        }

        public static IPolygonClient Create(string apiBaseUrl, string xApiKey)
        {
            return new PolygonClient(apiBaseUrl, xApiKey);
        }

        Task<EthereumAccountBalance> IPolygonClient.GetAccountBalance(string address)
        {
            return oneApi.GetAccountBalance(address);
        }

        Task<TransactionHash> IPolygonClient.SendTransactionKMS(TransferPolygonBlockchainKMS transfer)
        {
            return oneApi.SendTransactionKMS(transfer);
        }
    }
}
