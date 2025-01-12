﻿using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IOneApi
    {
        [Get("/v3/one/account/balance/{address}?shardID=0")]
        Task<OneBalance> GetBalance(string address);

        [Post("/v3/one/transaction?shardID=0")]
        Task<Signature> SendTransactionKMS(TransferOneBlockchainKMS transfer);

        [Get("/v3/one/address/{xpub}/{index}")]
        Task<BlockchainAddress> GenerateAddress(string xpub, int index);

        [Get("/v3/one/transaction/{hash}")]
        Task<PolygonTx> GetTransaction(string hash);
    }
}
