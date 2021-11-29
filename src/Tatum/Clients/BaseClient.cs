using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    //[CatchErrorsAttribute]
    public class BaseClient
    {
        public string NetworkName { get; set; }
        public string Currency { get; set; }
        public string ContractAddress { get; set; }
        public string ContractType { get; set; }
        public int DecimalPrecision { get; set; }

        public virtual Task<Signature> SendLedgerKMS(TransferLedgerKMS transfer)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<decimal> EstimateTransactionFee(TransferBlockchainKMS transfer)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<Signature> SendTransactionKMS(TransferBtcBasedKMS transfer)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<decimal> GetTransactionFee(string transactionHash)
        {
            throw new System.NotImplementedException();
        }

        public virtual Task<SimpleTransaction> GetSimpleTransaction(string transactionHash)
        {
            throw new System.NotImplementedException();
        }
    }
}
