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
    public interface IAdaApi
    {
        [Get("/v3/ada/account/{address}")]
        Task<AdaAccount> GetAccount(string address);

        [Post("/v3/ada/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferBtcBasedBlockchainKMS transfer);
    }
}
