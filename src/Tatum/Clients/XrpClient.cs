using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class XrpClient : BaseClient, IXrpClient
    {
        private readonly IXrpApi xrpApi;
        private static Precision Precision { get; } = Precision.Precision6;

        internal XrpClient(string apiBaseUrl, string xApiKey)
        {
            xrpApi = RestClientFactory.Create<IXrpApi>(apiBaseUrl, xApiKey);
        }

        public static IXrpClient Create(string apiBaseUrl, string xApiKey)
        {
            return new XrpClient(apiBaseUrl, xApiKey);
        }

        Task<TransactionHash> IXrpClient.Broadcast(BroadcastRequest request)
        {
            var validationContext = new ValidationContext(request);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);

            return xrpApi.Broadcast(request);
        }

        Task<XrpAccountBalance> IXrpClient.GetAccountBalance(string accountAddress)
        {
            return xrpApi.GetAccountBalance(accountAddress);
        }

        Task<XrpAccountInfo> IXrpClient.GetAccountInfo(string accountAddress)
        {
            return xrpApi.GetAccountInfo(accountAddress);
        }

        Task<XrpAccountTransactionsRoot> IXrpClient.GetAccountTransactions(string address, uint min, string marker)
        {
            return xrpApi.GetAccountTransactions(address, min, marker);
        }

        Task<XrpInfo> IXrpClient.GetBlockchainInfo()
        {
            return xrpApi.GetBlockchainInfo();
        }

        Task<XrpFee> IXrpClient.GetFee()
        {
            return xrpApi.GetFee();
        }

        Task<XrpLedgerRoot> IXrpClient.GetLedger(uint sequence)
        {
            return xrpApi.GetLedger(sequence);
        }

        Task<XrpTx> IXrpClient.GetTransaction(string hash)
        {
            return xrpApi.GetTransaction(hash);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var accountBalance = await xrpApi.GetAccountBalance(request.Address);
            return TatumHelper.ToDecimal(TatumHelper.ToLong(accountBalance.Balance), Precision);
        }

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var req = new TransferXrpBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                FromAccount = transfer.FromAddress,
                SourceTag = transfer.FromTag.Value,
                Fee = transfer.Fee.ToString(),
                Amount = transfer.Amount.ToString(),
                To = transfer.ToAddress,
                DestinationTag = transfer.ToTag.Value
            };
            var tx = await xrpApi.SendTransactionKMS(req);
            return tx;
        }

        public Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            return Task.FromResult(new GenerateAddressResponse() { Address = xPubString, TagId = index, BlockchainAddressType = BlockchainAddressType.TagId });
        }
    }
}
