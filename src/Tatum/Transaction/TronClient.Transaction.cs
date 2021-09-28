using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class TronClient : ITronClient
    {
        async Task<TransactionHash> ITronClient.SendTransactionKMS(TransferTronBlockchainKMS body)
        {
            return await tronApi.SendTransactionKMS(body);
        }
    }
}
