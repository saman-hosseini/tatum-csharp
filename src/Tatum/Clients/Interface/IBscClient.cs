using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public interface IBscClient : IBaseClient
    {
        Task<TransactionHash> SendTransactionKMS(TransferBscBlockchainKMS transfer);
        Task<EthereumAccountBalance> GetAccountBalance(string address);
    }
}
