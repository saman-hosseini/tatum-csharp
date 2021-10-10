using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface ICeloClient
    {
        Task<CeloBalance> GetBalance(string address);

        Task<Signature> SendTransactionKMS(TransferCeloBlockchainKMS transfer);
    }
}
