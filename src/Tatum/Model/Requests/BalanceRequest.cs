namespace TatumPlatform.Model.Requests
{
    public class BalanceRequest
    {
        public string Address { get; set; }
        public string Currency { get; set; }
        public string ContractAddress { get; set; }
    }
}
