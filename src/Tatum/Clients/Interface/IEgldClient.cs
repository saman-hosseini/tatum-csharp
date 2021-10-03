﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public interface IEgldClient
    {
        Task<EgldBalance> GetBalance(string address);

        Task<TransactionHash> SendTransactionKMS(TransferEgldBlockchainKMS transfer);
    }
}