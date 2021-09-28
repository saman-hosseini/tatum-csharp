using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IDogecoinApi
    {
        [Post("/v3/dogecoin/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferDogecoinBlockchainKMS transfer);
    }
}
