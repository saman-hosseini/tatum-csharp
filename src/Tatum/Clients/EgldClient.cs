using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class EgldClient : IEgldClient
    {
        private readonly IEgldApi egldApi;

        internal EgldClient()
        {
        }

        internal EgldClient(string apiBaseUrl, string xApiKey)
        {
            egldApi = RestClientFactory.Create<IEgldApi>(apiBaseUrl, xApiKey);
        }

        public static IEgldClient Create(string apiBaseUrl, string xApiKey)
        {
            return new EgldClient(apiBaseUrl, xApiKey);
        }

        Task<EgldBalance> IEgldClient.GetBalance(string address)
        {
            return egldApi.GetBalance(address);
        }

        Task<TransactionHash> IEgldClient.SendTransactionKMS(TransferEgldBlockchainKMS transfer)
        {
            return egldApi.SendTransactionKMS(transfer);
        }
    }
}
