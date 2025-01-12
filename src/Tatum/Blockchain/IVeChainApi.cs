﻿using Refit;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IVeChainApi
    {
        [Post("/v3/vet/broadcast")]
        Task<TransactionHash> Broadcast(BroadcastRequest request);

        [Post("/v3/vet/transaction/gas")]
        Task<long> EstimateGas(EstimateGasRequest request);

        [Get("/v3/vet/block/current")]
        Task<long> GetCurrentBlock();

        [Get("/v3/vet/block/{hash}")]
        Task<VeChainBlock> GetBlock(string hash);

        [Get("/v3/vet/account/balance/{address}")]
        Task<VeChainAccountBalance> GetBalance(string address);

        [Get("/v3/vet/account/energy/{address}")]
        Task<VeChainAccountEnergy> GetAccountEnergy(string address);

        [Get("/v3/vet/transaction/{hash}")]
        Task<VeChainTx> GetTransaction(string hash);

        [Get("/v3/vet/transaction/{hash}/receipt")]
        Task<VeChainTxReceipt> GetTransactionReceipt(string hash);

        [Post("/v3/vet/transaction")]
        Task<Signature> SendTransactionKMS(TransferVeChainBlockchainKMS transfer);

        [Get("/v3/vet/address/{xpub}/{index}")]
        Task<BlockchainAddress> GenerateAddress(string xpub, int index);
    }
}
