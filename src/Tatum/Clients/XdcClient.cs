using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class XdcClient : IXdcClient
    {
        private readonly IXdcApi xdcApi;

        internal XdcClient()
        {
        }

        internal XdcClient(string apiBaseUrl, string xApiKey)
        {
            xdcApi = RestClientFactory.Create<IXdcApi>(apiBaseUrl, xApiKey);
        }

        public static IXdcClient Create(string apiBaseUrl, string xApiKey)
        {
            return new XdcClient(apiBaseUrl, xApiKey);
        }

        Task<XdcBalance> IXdcClient.GetBalance(string address)
        {
            return xdcApi.GetBalance(address);
        }

        Task<TransactionHash> IXdcClient.SendTransactionKMS(TransferXdcBlockchainKMS transfer)
        {
            return xdcApi.SendTransactionKMS(transfer);
        }
    }
}
