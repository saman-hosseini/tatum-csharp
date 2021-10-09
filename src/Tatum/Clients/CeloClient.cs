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

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var accountBalance = await celoApi.GetBalance(request.Address);
            return TatumHelper.ToDecimal(accountBalance.Celo);
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var req = new TransferCeloBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                Index = transfer.Index,
                Amount = transfer.Amount.ToString(),
                Currency = transfer.Currency,
                FeeCurrency = transfer.Currency,
                To = transfer.ToAddress,
                Data = transfer.Message
            };
            var tx = await celoApi.SendTransactionKMS(req);
            return tx;
        }

        public async Task<string> GenerateAddress(string xPubString, int index)
        {
            var address = await celoApi.GenerateAddress(xPubString, index);
            return address.Address;
        }
    }
}
