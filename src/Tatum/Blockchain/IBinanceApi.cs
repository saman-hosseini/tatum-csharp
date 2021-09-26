using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatum.Model.Requests.Binance;
using Tatum.Model.Responses;

namespace Tatum.Blockchain
{
    public interface IBinanceApi
    {
        [Get("/v3/bnb/account/{address}")]
        Task<BnbAccount> GetAccountBalance(string address);

        [Post("/v3/bnb/transaction")]
        Task<TransactionHash> SendTransactionKMS(TransferBnbBlockchainKMS transfer);
    }
}
