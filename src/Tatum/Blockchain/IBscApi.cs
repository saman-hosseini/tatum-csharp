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

        [Get("/v3/bsc/account/balance/bep20/{address}?currency={currency}&contractAddress={contractAddress}")]
        Task<EthereumAccountBalance> GetBalance(string address, string currency, string contractAddress);

        [Get("/v3/bsc/address/{xpub}/{index}")]
        Task<BlockchainAddress> GenerateAddress(string xpub, int index);

        [Post("/v3/blockchain/token/transaction")]
        Task<Signature> SendTokenTransactionKMS(TransferBscTokenBlockchainKMS transferTokenKMS);

        [Post("/v3/bsc/gas")]
        Task<object> EstimateFee(EthereumEstimateFee estimateFee);
    }
}
