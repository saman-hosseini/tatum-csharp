namespace TatumPlatform.Model.Requests
{
    public class TransferBlockchainKMS
    {
        public string SenderAccountId { get; set; }
        public string XPub { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public int? FromTag { get; set; }
        public int? ToTag { get; set; }
        public decimal Amount { get; set; }
        public int Index { get; set; }
        public string Message { get; set; }
        public string SignatureId { get; set; }
    }
}
