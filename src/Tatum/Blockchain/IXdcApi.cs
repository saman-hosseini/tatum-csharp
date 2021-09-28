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
    public interface IXdcApi
    {
        [Get("/v3/xdc/account/balance/{address}")]
        Task<XdcBalance> GetBalance(string address);

        [Post("/v3/xdc/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferXdcBlockchainKMS transfer);

        [Post("/v3/xdc/gas")]
        Task<Fee> EstimateTransactionFee(TransferXdcBlockchainKMS transfer);
    }
}