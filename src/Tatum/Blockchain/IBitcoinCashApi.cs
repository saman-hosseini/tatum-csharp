using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IBitcoinCashApi
    {
        [Post("/v3/bcash/broadcast")]
        Task<TransactionHash> BroadcastSignedTransaction(BroadcastRequest request);

        [Get("/v3/bcash/info")]
        Task<BitcoinCashInfo> GetBlockchainInfo();

        [Get("/v3/bcash/block/{hash}")]
        Task<BitcoinCashBlock> GetBlock(string hash);

        [Get("/v3/bcash/block/hash/{blockHeight}")]
        Task<BlockHash> GetBlockHash(long blockHeight);

        [Get("/v3/bcash/transaction/address/{address}?skip={skip}")]
        Task<List<BitcoinCashTx>> GetTxForAccount(string address, int skip = 0);

        [Get("/v3/bcash/transaction/{hash}")]
        Task<BitcoinCashTx> GetTransaction(string hash);

        [Post("/v3/bcash/transaction")]
        Task<Signature> SendTransactionKMS(TransferBchBlockchainKMS transferBtc);
    }
}
