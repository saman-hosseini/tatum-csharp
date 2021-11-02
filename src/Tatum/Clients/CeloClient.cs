using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class CeloClient : BaseClient, ICeloClient
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

        Task<Signature> ICeloClient.SendTransactionKMS(TransferCeloBlockchainKMS transfer)
        {
            return celoApi.SendTransactionKMS(transfer);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var accountBalance = await celoApi.GetBalance(request.Address);
            if (Currency.ToUpper() == "CELO" )
                return TatumHelper.ToDecimal(accountBalance.Celo);
            if (Currency.ToUpper() == "CUSD")
                return TatumHelper.ToDecimal(accountBalance.CUsd);
            throw new System.Exception($"Celo network doesnt support {Currency}");
        }

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var req = new TransferCeloBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                Index = transfer.Index,
                Amount = transfer.Amount.ToString(),
                Currency = Currency,
                FeeCurrency = Currency,
                To = transfer.ToAddress,
                Data = transfer.Message
            };
            var tx = await celoApi.SendTransactionKMS(req);
            return tx;
        }

        public override async Task<decimal> GetTransactionFee(string transactionHash)
        {
            var tx = await celoApi.GetTransaction(transactionHash);
            return tx.Fee;
        }

        public async Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            var address = await celoApi.GenerateAddress(xPubString, index);
            return new GenerateAddressResponse() { Address = address.Address, BlockchainAddressType = BlockchainAddressType.ReceiveAddress };
        }
    }
}
