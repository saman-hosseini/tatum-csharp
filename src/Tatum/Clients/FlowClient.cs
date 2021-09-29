using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class FlowClient : IFlowClient
    {
        private readonly IFlowApi flowApi;

        internal FlowClient()
        {
        }

        internal FlowClient(string apiBaseUrl, string xApiKey)
        {
            flowApi = RestClientFactory.Create<IFlowApi>(apiBaseUrl, xApiKey);
        }

        public static IFlowClient Create(string apiBaseUrl, string xApiKey)
        {
            return new FlowClient(apiBaseUrl, xApiKey);
        }

        Task<FlowAccount> IFlowClient.GetAccount(string address)
        {
            return flowApi.GetAccountBalance(address);
        }

        Task<TransactionHash> IFlowClient.SendTransactionKMS(TransferFlowBlockchainKMS transfer)
        {
            return flowApi.SendTransactionKMS(transfer);
        }
    }
}
