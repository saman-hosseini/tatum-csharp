using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class PolygonClient : IPolygonClient
    {
        private readonly IPolygonApi polygonApi;
        private const int GasLimit = 21000;
        private static Precision Precision { get; } = Precision.Precision18;
        internal PolygonClient()
        {
        }

        internal PolygonClient(string apiBaseUrl, string xApiKey)
        {
            polygonApi = RestClientFactory.Create<IPolygonApi>(apiBaseUrl, xApiKey);
        }

        public static IPolygonClient Create(string apiBaseUrl, string xApiKey)
        {
            return new PolygonClient(apiBaseUrl, xApiKey);
        }

        Task<EthereumAccountBalance> IPolygonClient.GetAccountBalance(string address)
        {
            return polygonApi.GetBalance(address);
        }

        Task<TransactionHash> IPolygonClient.SendTransactionKMS(TransferPolygonBlockchainKMS transfer)
        {
            return polygonApi.SendTransactionKMS(transfer);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var balance = await polygonApi.GetBalance(request.Address);
            return TatumHelper.ToDecimal(balance.Balance);
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var gasPrice = (TatumHelper.ToLong(transfer.Fee, Precision) / GasLimit).ToString();
            var req = new TransferPolygonBlockchainKMS()
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
            var tx = await polygonApi.SendTransactionKMS(req);
            return tx;
        }

        public async Task<string> GenerateAddress(string xPubString, int index)
        {
            var address = await polygonApi.GenerateAddress(xPubString, index);
            return address.Address;
        }
    }
}
