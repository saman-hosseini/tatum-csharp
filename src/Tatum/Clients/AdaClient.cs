using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatum.Blockchain;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Clients
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
    }
}
