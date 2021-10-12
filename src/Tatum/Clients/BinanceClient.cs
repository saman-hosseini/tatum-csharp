using System.Linq;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class BinanceClient : IBinanceClient
    {
        private readonly IBinanceApi bnbApi;

        internal BinanceClient()
        {
        }

        internal BinanceClient(string apiBaseUrl, string xApiKey)
        {
            bnbApi = RestClientFactory.Create<IBinanceApi>(apiBaseUrl, xApiKey);
        }

        public static IBinanceClient Create(string apiBaseUrl, string xApiKey)
        {
            return new BinanceClient(apiBaseUrl, xApiKey);
        }

        Task<BnbAccount> IBinanceClient.GetAccount(string address)
        {
            return bnbApi.GetAccount(address);
        }

        Task<Signature> IBinanceClient.SendTransactionKMS(TransferBnbBlockchainKMS transfer)
        {
            return bnbApi.SendTransactionKMS(transfer);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var account = await bnbApi.GetAccount(request.Address);
            var balance = account.Balances.Where(x => x.Symbol == request.Currency).Sum(q => TatumHelper.ToDecimal(q.Free));
            return balance;
        }

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var req = new TransferBnbBlockchainKMS()
            {
                Amount = transfer.Amount.ToString(),
                Currency = transfer.Currency,
                FromAddress = transfer.FromAddress,
                SignatureId = transfer.SignatureId,
                To = transfer.ToAddress,
                Message = transfer.Message
            };
            var tx = await bnbApi.SendTransactionKMS(req);
            return tx;
        }

        public Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            throw new System.NotImplementedException();
        }
    }
}
