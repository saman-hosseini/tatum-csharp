using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Clients
{
    public interface IBscClient
    {
        Task<TransactionHash> SendTransactionKMS(TransferBscBlockchainKMS transfer);
        Task<EthereumAccountBalance> GetAccountBalance(string address);
    }
}
