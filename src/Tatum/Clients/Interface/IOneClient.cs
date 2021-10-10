using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public interface IOneClient : IBaseClient
    {
        Task<OneBalance> GetBalance(string address);

        Task<Signature> SendTransactionKMS(TransferOneBlockchainKMS transfer);
    }
}
