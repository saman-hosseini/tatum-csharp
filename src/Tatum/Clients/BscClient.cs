using System;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class BscClient : IBscClient
    {
        private readonly IBscApi bscApi;
        private const int GasLimit = 21000;
        private static Precision Precision { get; } = Precision.Precision18;

        internal BscClient()
        {
        }

        internal BscClient(string apiBaseUrl, string xApiKey)
        {
            bscApi = RestClientFactory.Create<IBscApi>(apiBaseUrl, xApiKey);
        }

        public static IBscClient Create(string apiBaseUrl, string xApiKey)
        {
            return new BscClient(apiBaseUrl, xApiKey);
        }

        Task<EthereumAccountBalance> IBscClient.GetAccountBalance(string address)
        {
            return bscApi.GetAccountBalance(address);
        }

        Task<TransactionHash> IBscClient.SendTransactionKMS(TransferBscBlockchainKMS transfer)
        {
            return bscApi.SendTransactionKMS(transfer);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var balance = await bscApi.GetAccountBalance(request.Address);
            return TatumHelper.ToDecimal(balance.Balance);
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var gasPrice = (TatumHelper.ToLong(transfer.Fee, Precision) / GasLimit).ToString();
            var req = new TransferBscBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                Amount = transfer.Amount.ToString(),
                Currency = transfer.Currency,
                Fee = new Fee()
                {
                    GasLimit = GasLimit.ToString(),
                    GasPrice = gasPrice
                },
                To = transfer.ToAddress,
                Index = transfer.Index,
                Data = transfer.Message
            };
            var tx = await bscApi.SendTransactionKMS(req);
            return tx;
        }

        public async Task<string> GenerateAddress(string xPubString, int index)
        {
            var address = await bscApi.GenerateAddress(xPubString, index);
            return address.Address;
        }
    }
}
