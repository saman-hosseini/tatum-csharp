using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public interface IBaseClient
    {
        Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer);
        Task<decimal> GetBalance(BalanceRequest request);
        Task<string> GenerateAddress(string xPubString, int index);
    }
}
