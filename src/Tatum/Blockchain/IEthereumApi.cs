using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IEthereumApi
    {
        [Post("/v3/ethereum/broadcast")]
        Task<TransactionHash> BroadcastSignedTransaction(BroadcastRequest request);

        [Get("/v3/ethereum/transaction/count/{address}")]
        Task<int> GetTransactionsCount(string address);

        [Get("/v3/ethereum/block/current")]
        Task<long> GetCurrentBlock();

        [Get("/v3/ethereum/block/{hash}")]
        Task<EthereumBlock> GetBlock(string hash);

        [Get("/v3/ethereum/account/balance/{address}")]
        Task<EthereumAccountBalance> GetAccountBalance(string address);

        [Get("/v3/ethereum/account/balance/erc20/{address}?currency={currency}&contractAddress={contractAddress}")]
        Task<EthereumAccountBalance> GetErc20AccountBalance(string address, string currency, string contractAddress);

        [Post("/v3/ethereum/transaction")]
        Task<TransactionHash> SendTransaction(TransferEthereumErc20 transfer);

        [Post("/v3/ethereum/transaction")]
        Task<Signature> SendTransactionKMS(TransferEthereumErc20KMS transfer);

        [Get("/v3/ethereum/transaction/{hash}")]
        Task<EthereumTx> GetTransaction(string hash);

        [Get("/v3/ethereum/account/transaction/{address}?pageSize={pageSize}&offset={offset}")]
        Task<List<EthereumTx>> GetAccountTransactions(string address, int pageSize = 50, int offset = 0);

        [Get("/v3/ethereum/address/{xpub}/{index}")]
        Task<BlockchainAddress> GenerateAddress(string xpub, int index);

        [Post("/v3/blockchain/token/transaction")]
        Task<Signature> SendTokenTransactionKMS(TransferEthereumTokenErc20KMS transfer);
    }
}
