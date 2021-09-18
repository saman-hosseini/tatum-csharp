using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Tatum.Blockchain;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Clients
{
    public partial class TronClient : ITronClient
    {
        private readonly ITronApi tronApi;

        internal TronClient()
        {
        }

        internal TronClient(string apiBaseUrl, string xApiKey)
        {
            tronApi = RestClientFactory.Create<ITronApi>(apiBaseUrl, xApiKey);
        }

        public static ITronClient Create(string apiBaseUrl, string xApiKey)
        {
            return new TronClient(apiBaseUrl, xApiKey);
        }

        Task<TransactionHash> ITronClient.Broadcast(BroadcastRequest request)
        {
            var validationContext = new ValidationContext(request);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);

            return tronApi.BroadcastSignedTransaction(request);
        }

        Task<TronInfo> ITronClient.GetBlockchainInfo()
        {
            return tronApi.GetBlockchainInfo();
        }

        Task<TronBlock> ITronClient.GetBlock(string hash)
        {
            return tronApi.GetBlock(hash);
        }

        Task<TronAccount> ITronClient.GetTronAccount(string address)
        {
            return tronApi.GetTronAccountbyAddress(address);
        }
    }
}
