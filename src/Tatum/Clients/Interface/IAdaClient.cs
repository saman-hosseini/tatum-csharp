﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public interface IAdaClient : IBaseClient
    {
        Task<AdaAccount> GetAccount(string address);
        Task<Signature> SendTransactionKMS(TransferBtcBasedBlockchainKMS transfer);
    }
}
