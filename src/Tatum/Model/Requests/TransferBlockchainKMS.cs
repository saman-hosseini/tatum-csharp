﻿namespace TatumPlatform.Model.Requests
{
    public class TransferBlockchainKMS
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
        public string Currency { get; set; }
        public string ContractAddress { get; set; }
        public int Index { get; set; }
        public string SignatureId { get; set; }
    }
}