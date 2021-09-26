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
    public interface IPolygonApi
    {
        [Post("/v3/polygon/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferPolygonBlockchainKMS transferKMS);

        [Get("/v3/polygon/account/balance/{address}")]
        Task<EthereumAccountBalance> GetAccountBalance(string address);
    }
}
