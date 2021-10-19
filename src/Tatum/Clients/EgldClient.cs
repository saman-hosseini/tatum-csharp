using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class EgldClient : BaseClient, IEgldClient
    {
        private readonly IEgldApi egldApi;
        private const int GasLimit = 21000;
        private static Precision Precision { get; } = Precision.Precision18;
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

        Task<Signature> IEgldClient.SendTransactionKMS(TransferEgldBlockchainKMS transfer)
        {
            return egldApi.SendTransactionKMS(transfer);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var balance = await egldApi.GetBalance(request.Address);
            return TatumHelper.ToDecimal(balance.Balance);
        }

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            //var gasPrice = (TatumHelper.ToLong(transfer.Fee, Precision) / GasLimit).ToString();
            var req = new TransferEgldBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                Amount = transfer.Amount.ToString(),
                Data = transfer.Message,
                Index = transfer.Index,
                To = transfer.ToAddress,
                //Fee = new Fee()
                //{
                //    GasLimit = GasLimit.ToString(),
                //    GasPrice = gasPrice
                //},
                From = transfer.FromAddress
            };
            var tx = await egldApi.SendTransactionKMS(req);
            return tx;
        }

        public async Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            throw new System.NotImplementedException();
        }
    }
}
