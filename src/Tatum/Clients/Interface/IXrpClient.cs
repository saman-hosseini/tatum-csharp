using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public interface IXrpClient : IBaseClient
    {
        Task<XrpAccountInfo> GetAccountInfo(string accountAddress);
        Task<TransactionHash> Broadcast(BroadcastRequest request);
        Task<XrpInfo> GetBlockchainInfo();
        Task<XrpFee> GetFee();
        Task<XrpLedgerRoot> GetLedger(uint sequence);
        Task<XrpAccountBalance> GetAccountBalance(string accountAddress);
        Task<XrpTx> GetTransaction(string hash);
        Task<XrpAccountTransactionsRoot> GetAccountTransactions(string address, uint min = 0, string marker = null);
    }
}
