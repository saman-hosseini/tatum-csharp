namespace TatumPlatform.Model.Requests
{
    public class TransferBtcBasedKMS
    {
        public string SenderAccountId { get; set; }
        public string XPub { get; set; }
        public string[] ToAddresses { get; set; }
        public decimal Amount { get; set; }
        public string[] MultipleAmounts { get; set; }
        public string Message { get; set; }
        public string SignatureId { get; set; }
    }
}
