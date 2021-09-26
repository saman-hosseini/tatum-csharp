using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Clients
{
    public interface IDogecoinClient
    {
        Task<TransactionHash> SendTransactionKMS(TransferDogecoinBlockchainKMS transfer);
        Task<DogecoinBalance> GetBalance(string address);
    }
}
