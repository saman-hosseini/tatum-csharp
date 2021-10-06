using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class EgldClient : IEgldClient
    {
        private readonly IEgldApi egldApi;
        private const int GasLimit = 21000;

        internal EgldClient()
        {
        }

        internal EgldClient(string apiBaseUrl, string xApiKey)
        {
            egldApi = RestClientFactory.Create<IEgldApi>(apiBaseUrl, xApiKey);
        }

        public static IEgldClient Create(string apiBaseUrl, string xApiKey)
        {
            return new EgldClient(apiBaseUrl, xApiKey);
        }

        Task<EgldBalance> IEgldClient.GetBalance(string address)
        {
            return egldApi.GetBalance(address);
        }

        Task<TransactionHash> IEgldClient.SendTransactionKMS(TransferEgldBlockchainKMS transfer)
        {
            return egldApi.SendTransactionKMS(transfer);
        }

        private static decimal ToDecimalEgld(long amount)
        {
            return amount / 1000000000000000000M;
        }

        private static long ToLongEgld(decimal amount)
        {
            return decimal.ToInt64(amount * 1000000000000000000);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var balance = await egldApi.GetBalance(request.Address);
            return TatumHelper.ToDecimal(balance.Balance);
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var gasPrice = (ToLongEgld(transfer.Fee) / GasLimit).ToString();
            var req = new TransferEgldBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                Amount = transfer.Amount.ToString(),
                Data = transfer.Message,
                Index = transfer.Index,
                To = transfer.ToAddress,
                Fee = new Fee()
                {
                    GasLimit = GasLimit.ToString(),
                    GasPrice = gasPrice
                },
                From = transfer.FromAddress
            };
            var tx = await egldApi.SendTransactionKMS(req);
            return tx;
        }
    }
}
