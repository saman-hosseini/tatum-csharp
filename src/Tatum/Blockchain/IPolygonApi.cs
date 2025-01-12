﻿using Refit;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IPolygonApi
    {
        [Post("/v3/polygon/transaction")]
        Task<Signature> SendTransactionKMS(TransferPolygonBlockchainKMS transferKMS);

        [Get("/v3/polygon/account/balance/{address}")]
        Task<EthereumAccountBalance> GetBalance(string address);

        [Get("/v3/polygon/address/{xpub}/{index}")]
        Task<BlockchainAddress> GenerateAddress(string xpub, int index);

        [Get("/v3/polygon/transaction/{hash}")]
        Task<PolygonTx> GetTransaction(string hash);
    }
}
