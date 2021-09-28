using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests.Binance;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IBinanceApi
    {
        [Get("/v3/bnb/account/{address}")]
        Task<BnbAccount> GetAccountBalance(string address);

        [Post("/v3/bnb/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferBnbBlockchainKMS transfer);
    }
}
