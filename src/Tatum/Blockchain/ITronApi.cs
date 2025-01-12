﻿using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface ITronApi
    {
        [Get("/v3/tron/wallet?mnemonic={mnemonic}")]
        Task<Wallet> GenerateTronWallet(string mnemonic);

        [Post("/v3/tron/wallet/priv")]
        Task<TronPrivateKey> GenerateTronPrivateKey(TronPrivateKeyRequest request);

        [Get("/v3/tron/address/{xpub}/{index}")]
        Task<BlockchainAddress> GenerateAddress(string xpub, int index);

        [Get("/v3/tron/account/{address}")]
        Task<TronAccount> GetAccount(string address);

        [Post("/v3/tron/broadcast")]
        Task<TransactionHash> BroadcastSignedTransaction(BroadcastRequest request);

        [Get("/v3/tron/info")]
        Task<TronInfo> GetBlockchainInfo();

        [Get("/v3/tron/block/{hash}")]
        Task<TronBlock> GetBlock(string hash);

        [Post("/v3/tron/transaction")]
        Task<Signature> SendTransactionKMS(TransferTronBlockchainKMS transfer);

        [Post("/v3/tron/trc20/transaction")]
        Task<Signature> SendTrc20TransactionKMS(TransferTronTrc20BlockchainKMS transfer);

        [Post("/v3/tron/trc10/transaction")]
        Task<Signature> SendTrc10TransactionKMS(TransferTronTrc10BlockchainKMS transfer);

        [Get("/v3/tron/transaction/{hash}")]
        Task<TronTx> GetTransaction(string hash);

        [Get("/v3/tron/transaction/account/{address}")]
        Task<TronTxs> GetAccountTransactions(string address, string next = "");
    }
}
