using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Clients
{
    public interface IAdaClient
    {
        Task<AdaAccount> GetAccount(string address);
        Task<TransactionHash> SendTransactionKMS(TransferBtcBasedBlockchainKMS transfer);
    }
}
