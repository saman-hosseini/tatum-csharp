using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IBitcoinApi
    {
        [Post("/v3/bitcoin/broadcast")]
        Task<TransactionHash> BroadcastSignedTransaction(BroadcastRequest request);

        [Get("/v3/bitcoin/info")]
        Task<BitcoinInfo> GetBlockchainInfo();

        [Get("/v3/bitcoin/block/{hash}")]
        Task<BitcoinBlock> GetBlock(string hash);

        [Get("/v3/bitcoin/block/hash/{blockHeight}")]
        Task<BlockHash> GetBlockHash(long blockHeight);

        [Get("/v3/bitcoin/utxo/{txHash}/{txOutputIndex}")]
        Task<BitcoinUtxo> GetUtxo(string txHash, int txOutputIndex);

        [Get("/v3/bitcoin/transaction/address/{address}?pageSize={pageSize}&offset={offset}")]
        Task<List<BitcoinTx>> GetTxForAccount(string address, int pageSize = 50, int offset = 0);

        [Get("/v3/bitcoin/transaction/{hash}")]
        Task<BitcoinTx> GetTransaction(string hash);


        [Post("/v3/bitcoin/transaction")]
        Task<TransactionHash> SendTransaction(TransferBtcBasedBlockchain transfer);

        [Post("/v3/bitcoin/transaction")]
        Task<Signature> SendTransactionKMS(TransferBtcBasedBlockchainKMS transfer);

        [Get("/v3/bitcoin/address/balance/{address}")]
        Task<BitcoinAccountBalance> GetAccountBalance(string address);

        [Get("/v3/bitcoin/address/{xpub}/{index}")]
        Task<BlockchainAddress> GenerateAddress(string xpub, int index);

        [Post("/v3/offchain/blockchain/estimate")]
        Task<EstimatedFee> EstimateFee(BitcoinEstimateFee estimateFee);
    }
}
