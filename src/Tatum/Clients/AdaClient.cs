using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class AdaClient : IAdaClient
    {
        private readonly IAdaApi adaApi;

        internal AdaClient()
        {
        }

        internal AdaClient(string apiBaseUrl, string xApiKey)
        {
            adaApi = RestClientFactory.Create<IAdaApi>(apiBaseUrl, xApiKey);
        }

        public static IAdaClient Create(string apiBaseUrl, string xApiKey)
        {
            return new AdaClient(apiBaseUrl, xApiKey);
        }

        Task<AdaAccount> IAdaClient.GetAccount(string address)
        {
            return adaApi.GetAccount(address);
        }

        Task<TransactionHash> IAdaClient.SendTransactionKMS(TransferBtcBasedBlockchainKMS transfer)
        {
            return adaApi.SendTransactionKMS(transfer);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var account = await adaApi.GetAccount(request.Address);
            return 0M;
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            throw new NotImplementedException();
        }
    }
}
