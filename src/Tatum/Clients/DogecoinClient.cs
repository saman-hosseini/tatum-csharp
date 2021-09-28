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
    public class DogecoinClient : IDogecoinClient
    {
        private readonly IDogecoinApi dogecoinApi;
        private readonly IDogechainApi dogechainApi;
        private const string dogechainUrl = "https://dogechain.info/";
        internal DogecoinClient()
        {
            
        }

        internal DogecoinClient(string apiBaseUrl, string xApiKey)
        {
            dogecoinApi = RestClientFactory.Create<IDogecoinApi>(apiBaseUrl, xApiKey);
            dogechainApi = RestClientFactory.Create<IDogechainApi>(dogechainUrl);
        }

        public static IDogecoinClient Create(string apiBaseUrl, string xApiKey)
        {
            return new DogecoinClient(apiBaseUrl, xApiKey);
        }

        Task<DogecoinBalance> IDogecoinClient.GetBalance(string address)
        {
            return dogechainApi.GetBalance(address);
        }

        Task<TransactionHash> IDogecoinClient.SendTransactionKMS(TransferDogecoinBlockchainKMS transfer)
        {
            return dogecoinApi.SendTransactionKMS(transfer);
        }
    }
}
