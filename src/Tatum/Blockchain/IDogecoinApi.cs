using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Blockchain
{
    public interface IDogecoinApi
    {
        [Post("/v3/dogecoin/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferDogecoinBlockchainKMS transfer);
    }
}
