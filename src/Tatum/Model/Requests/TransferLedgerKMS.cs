using System.Collections.Generic;

namespace TatumPlatform.Model.Requests
{
    public class TransferLedgerKMS
    {
        public string SignatureId { get; set; }
        public string SenderAccountId { get; set; }
        public string ToAddress { get; set; }
        public decimal Amount { get; set; }
        public string XPub { get; set; }
        public List<string> MultipleAmounts { get; set; }
    }
}
