using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Clients
{
    public partial class TronClient : ITronClient
    {
        async Task<TransactionHash> ITronClient.SendTransactionKMS(TransferTronBlockchainKMS body)
        {
            return await tronApi.SendTransactionKMS(body);
        }
    }
}
