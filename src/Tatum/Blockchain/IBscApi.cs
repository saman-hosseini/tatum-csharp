using Refit;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IBscApi
    {
        [Post("/v3/bsc/transaction")]
        Task<Signature> SendTransactionKMS(TransferBscBlockchainKMS transferKMS);

        [Get("/v3/bsc/account/balance/{address}")]
        Task<EthereumAccountBalance> GetAccountBalance(string address);

        [Get("/v3/bsc/address/{xpub}/{index}")]
        Task<BlockchainAddress> GenerateAddress(string xpub, int index);
    }
}
