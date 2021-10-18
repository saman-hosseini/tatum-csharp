namespace TatumPlatform.Model.Requests
{
    public class TransferBlockchainKMS
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public int Index { get; set; }
        public string SignatureId { get; set; }
        public int? FromTag { get; set; }
        public int? ToTag { get; set; }
        public string Message { get; set; }
    }
}
