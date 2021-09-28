using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class TatumClient : ITatumClient
    {
        Task<OffchainTransactionResult> ITatumClient.OffchainTransferTron(OffchainTransferTron offchainTransfer)
        {
            return tatumApi.OffchainTransferTron(offchainTransfer);
        }
    }
}
