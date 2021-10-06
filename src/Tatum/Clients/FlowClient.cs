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
            return flowApi.GetAccount(address);
        }

        Task<TransactionHash> IFlowClient.SendTransactionKMS(TransferFlowBlockchainKMS transfer)
        {
            return flowApi.SendTransactionKMS(transfer);
        }

        private static decimal ToDecimalFlow(long amount)
        {
            return amount / 100000000M;
        }

        private static long ToLongFlow(decimal amount)
        {
            return decimal.ToInt64(amount * 100000000);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var balance = await flowApi.GetAccount(request.Address);
            return ToDecimalFlow(balance.Balance);
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var req = new TransferFlowBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                Amount = transfer.Amount.ToString(),
                Currency = transfer.Currency,
                Index = transfer.Index,
                To = transfer.ToAddress,
                Account = transfer.FromAddress
            };
            var tx = await flowApi.SendTransactionKMS(req);
            return tx;
        }
    }
}
