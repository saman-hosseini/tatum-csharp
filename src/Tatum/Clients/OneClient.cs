using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class OneClient : IOneClient
    {
        private readonly IOneApi oneApi;
        private const int GasLimit = 21000;

        internal OneClient()
        {
        }

        internal OneClient(string apiBaseUrl, string xApiKey)
        {
            oneApi = RestClientFactory.Create<IOneApi>(apiBaseUrl, xApiKey);
        }

        public static IOneClient Create(string apiBaseUrl, string xApiKey)
        {
            return new OneClient(apiBaseUrl, xApiKey);
        }

        Task<OneBalance> IOneClient.GetBalance(string address)
        {
            return oneApi.GetBalance(address);
        }

        Task<TransactionHash> IOneClient.SendTransactionKMS(TransferOneBlockchainKMS transfer)
        {
            return oneApi.SendTransactionKMS(transfer);
        }

        private static decimal ToDecimalOne(long amount)
        {
            return amount / 1000000000000000000M;
        }

        private static long ToLongOne(decimal amount)
        {
            return decimal.ToInt64(amount * 1000000000000000000);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var balance = await oneApi.GetBalance(request.Address);
            return TatumHelper.ToDecimal(balance.Balance);
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var gasPrice = (ToLongOne(transfer.Fee) / GasLimit).ToString();
            var req = new TransferOneBlockchainKMS()
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
            var tx = await oneApi.SendTransactionKMS(req);
            return tx;
        }
    }
}
