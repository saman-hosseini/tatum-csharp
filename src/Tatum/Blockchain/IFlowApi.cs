using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IFlowApi
    {
        [Get("/v3/flow/account/{address}")]
        Task<FlowAccount> GetAccountBalance(string address);

        [Post("/v3/flow/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferFlowBlockchainKMS transfer);
    }
}
