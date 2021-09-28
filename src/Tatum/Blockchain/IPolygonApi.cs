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
    public interface IPolygonApi
    {
        [Post("/v3/polygon/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferPolygonBlockchainKMS transferKMS);

        [Get("/v3/polygon/account/balance/{address}")]
        Task<EthereumAccountBalance> GetAccountBalance(string address);
    }
}
