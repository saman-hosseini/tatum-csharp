using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Blockchain
{
    public interface ITronApi
    {
        [Get("/v3/tron/wallet?mnemonic={mnemonic}")]
        Task<Wallet> GenerateTronWallet(string mnemonic);

        [Post("/v3/tron/wallet/priv")]
        Task<TronPrivateKey> GenerateTronPrivateKey(TronPrivateKeyRequest request);

        [Get("/v3/tron/address/{xpub}/{index}")]
        Task<TronAddress> GenerateTronAddress(string xpub, int index);

        [Get("/v3/tron/account/{address}")]
        Task<TronAccount> GetTronAccountbyAddress(string address);

        [Post("/v3/tron/broadcast")]
        Task<TransactionHash> BroadcastSignedTransaction(BroadcastRequest request);

        [Get("/v3/tron/info")]
        Task<TronInfo> GetBlockchainInfo();

        [Get("/v3/tron/block/{hash}")]
        Task<TronBlock> GetBlock(string hash);

        [Post("/v3/tron/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferTronBlockchainKMS transferTronKMS);
    }
}
