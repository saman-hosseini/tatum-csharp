using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class CeloClient : ICeloClient
    {
        private readonly ICeloApi celoApi;

        internal CeloClient()
        {
        }

        internal CeloClient(string apiBaseUrl, string xApiKey)
        {
            celoApi = RestClientFactory.Create<ICeloApi>(apiBaseUrl, xApiKey);
        }

        public static ICeloClient Create(string apiBaseUrl, string xApiKey)
        {
            return new CeloClient(apiBaseUrl, xApiKey);
        }

        Task<CeloBalance> ICeloClient.GetBalance(string address)
        {
            return celoApi.GetBalance(address);
        }

        Task<TransactionHash> ICeloClient.SendTransactionKMS(TransferCeloBlockchainKMS transfer)
        {
            return celoApi.SendTransactionKMS(transfer);
        }
    }
}
