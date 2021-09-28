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
    public interface ICeloApi
    {
        [Get("/v3/celo/account/balance/{address}")]
        Task<CeloBalance> GetBalance(string address);

        [Post("/v3/celo/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferCeloBlockchainKMS transfer);
    }
}
