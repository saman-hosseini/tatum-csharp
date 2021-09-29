using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class OneClient : IOneClient
    {
        private readonly IOneApi oneApi;

        internal OneClient()
        {
        }

        internal OneClient(string apiBaseUrl, string xApiKey)
        {
            oneApi = RestClientFactory.Create<IOneApi>(apiBaseUrl, xApiKey);
        }

        public static IOneClient Create(string apiBaseUrl, string xApiKey)
        {
            return new OneClient(apiBaseUrl, xApiKey);
        }

        Task<OneBalance> IOneClient.GetBalance(string address)
        {
            return oneApi.GetBalance(address);
        }

        Task<TransactionHash> IOneClient.SendTransactionKMS(TransferOneBlockchainKMS transfer)
        {
            return oneApi.SendTransactionKMS(transfer);
        }
    }
}
