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
    public class DogecoinClient : IDogecoinClient
    {
        private readonly IDogecoinApi dogecoinApi;
        private readonly IDogechainApi dogechainApi;

        internal DogecoinClient()
        {
            
        }

        internal DogecoinClient(string apiBaseUrl, string xApiKey)
        {
            dogecoinApi = RestClientFactory.Create<IDogecoinApi>(apiBaseUrl, xApiKey);
            dogechainApi = RestClientFactory.Create<IDogechainApi>("https://dogechain.info/");
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
