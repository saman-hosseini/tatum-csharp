using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public interface IPolygonClient : IBaseClient
    {
        Task<TransactionHash> SendTransactionKMS(TransferPolygonBlockchainKMS transferKMS);

        Task<EthereumAccountBalance> GetAccountBalance(string address);
    }
}
