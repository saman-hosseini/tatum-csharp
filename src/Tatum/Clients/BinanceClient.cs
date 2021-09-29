using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class BinanceClient: IBinanceClient
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

        Task<TransactionHash> IBinanceClient.SendTransactionKMS(TransferBnbBlockchainKMS transfer)
        {
            return bnbApi.SendTransactionKMS(transfer);
        }
    }
}
