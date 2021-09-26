using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatum.Model.Requests.One;
using Tatum.Model.Responses;

namespace Tatum.Blockchain
{
    public interface IOneApi
    {
        [Get("/v3/one/account/balance/{address}?shardID=0")]
        Task<OneBalance> GetBalance(string address);

        [Post("/v3/one/transaction?shardID=0")]
        Task<TransactionHash> SendTransactionKMS(TransferOneBlockchainKMS transfer);
    }
}
