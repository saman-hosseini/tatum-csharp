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
    public interface IEgldApi
    {
        [Get("/v3/egld/account/balance/{address}")]
        Task<EgldBalance> GetBalance(string address);

        [Post("/v3/egld/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferEgldBlockchainKMS transfer);
    }
}
