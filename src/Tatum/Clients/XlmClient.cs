using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class XlmClient : IXlmClient
    {
        private readonly IXlmApi xlmApi;
        private const string CoinName = "XLM";

        internal XlmClient()
        {
        }

        internal XlmClient(string apiBaseUrl, string xApiKey)
        {
            xlmApi = RestClientFactory.Create<IXlmApi>(apiBaseUrl, xApiKey);
        }

        public static IXlmClient Create(string apiBaseUrl, string xApiKey)
        {
            return new XlmClient(apiBaseUrl, xApiKey);
        }

        Task<TransactionHash> IXlmClient.Broadcast(BroadcastRequest request)
        {
            var validationContext = new ValidationContext(request);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);

            return xlmApi.Broadcast(request);
        }

        Task<XlmAccountInfo> IXlmClient.GetAccountInfo(string accountAddress)
        {
            return xlmApi.GetAccountInfo(accountAddress);
        }

        Task<List<XlmTx>> IXlmClient.GetAccountTransactions(string address)
        {
            return xlmApi.GetAccountTransactions(address);
        }

        Task<XlmInfo> IXlmClient.GetBlockchainInfo()
        {
            return xlmApi.GetBlockchainInfo();
        }

        Task<long> IXlmClient.GetFee()
        {
            return xlmApi.GetFee();
        }

        Task<List<XlmTx>> IXlmClient.GetLedger(string sequence)
        {
            return xlmApi.GetLedger(sequence);
        }

        Task<XlmTx> IXlmClient.GetLedgerTransaction(string sequence)
        {
            return xlmApi.GetLedgerTransaction(sequence);
        }

        Task<XlmTx> IXlmClient.GetTransaction(string hash)
        {
            return xlmApi.GetTransaction(hash);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var accountInfo = await xlmApi.GetAccountInfo(request.Address);
            string strBalance;
            if (request.Currency.ToUpper() == CoinName)
            {
                strBalance = accountInfo.Balances.FirstOrDefault(q => q.AssetCode == null && q.AssetIssuer == null).Balance;
            }
            else
            {
                strBalance = accountInfo.Balances.FirstOrDefault(q => q.AssetCode == request.Currency && q.AssetIssuer == request.ContractAddress).Balance;
            }
            return TatumHelper.ToDecimal(strBalance);
        }

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var req = new TransferXlmBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                Amount = transfer.Amount.ToString(),
                To = transfer.ToAddress,
                FromAccount = transfer.FromAddress,
                Message = transfer.Message
            };
            var tx = await xlmApi.SendTransactionKMS(req);
            return tx;
        }

        public Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            return Task.FromResult(new GenerateAddressResponse() { Address = xPubString, TagId = index, BlockchainAddressType = BlockchainAddressType.TagId });
        }
    }
}
