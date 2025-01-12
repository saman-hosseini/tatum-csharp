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
    public interface ICeloApi
    {
        [Get("/v3/celo/account/balance/{address}")]
        Task<CeloBalance> GetBalance(string address);

        [Post("/v3/celo/transaction")]
        Task<Signature> SendTransactionKMS(TransferCeloBlockchainKMS transfer);

        [Get("/v3/celo/address/{xpub}/{index}")]
        Task<BlockchainAddress> GenerateAddress(string xpub, int index);

        [Get("/v3/celo/transaction/{hash}")]
        Task<CeloTx> GetTransaction(string hash);
    }
}
