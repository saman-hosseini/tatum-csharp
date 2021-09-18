using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Clients
{
    public partial class TatumClient : ITatumClient
    {
        Task<OffchainTransactionResult> ITatumClient.OffchainTransferTron(OffchainTransfer offchainTransfer)
        {
            return tatumApi.OffchainTransferTron(offchainTransfer);
        }
    }
}
