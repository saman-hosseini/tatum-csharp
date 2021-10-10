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
    public interface IBinanceApi
    {
        [Get("/v3/bnb/block/current")]
        Task<long> GetCurrentBlock();

        [Get("/v3/bnb/account/{address}")]
        Task<BnbAccount> GetAccount(string address);

        [Post("/v3/bnb/transaction")]
        Task<Signature> SendTransactionKMS(TransferBnbBlockchainKMS transfer);
    }
}
