using Refit;
using System.Threading.Tasks;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Blockchain
{
    public interface IBscApi
    {
        [Post("/v3/bsc/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferBscBlockchainKMS transferKMS);

        [Get("/v3/bsc/account/balance/{address}")]
        Task<EthereumAccountBalance> GetAccountBalance(string address);
    }
}
