using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class XdcClient : IXdcClient
    {
        private readonly IXdcApi xdcApi;
        private const int GasLimit = 21000;
        private static Precision Precision { get; } = Precision.Precision18;
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

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var balance = await xdcApi.GetBalance(request.Address);
            return TatumHelper.ToDecimal(balance.Balance);
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var gasPrice = (TatumHelper.ToLong(transfer.Fee, Precision) / GasLimit).ToString();
            var req = new TransferXdcBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                Amount = transfer.Amount.ToString(),
                Currency = transfer.Currency,
                Data = transfer.Message,
                Index = transfer.Index,
                To = transfer.ToAddress,
                Fee = new Fee()
                {
                    GasLimit = GasLimit.ToString(),
                    GasPrice = gasPrice
                }
            };
            var tx = await xdcApi.SendTransactionKMS(req);
            return tx;
        }

        public async Task<string> GenerateAddress(string xPubString, int index)
        {
            var address = await xdcApi.GenerateAddress(xPubString, index);
            return address.Address;
        }
    }
}
