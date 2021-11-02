using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public interface IBaseClient
    {
        string NetworkName { get; set; }
        string Currency { get; set; }
        string ContractAddress { get; set; }
        string ContractType { get; set; }
        int DecimalPrecision { get; set; }
        Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer);
        Task<Signature> SendLedgerKMS(TransferLedgerKMS transfer);
        Task<decimal> GetBalance(BalanceRequest request);
        Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index);
        Task<decimal> GetTransactionFee(string transactionHash);
    }
}
