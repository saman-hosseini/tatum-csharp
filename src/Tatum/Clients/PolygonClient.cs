﻿using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class PolygonClient : BaseClient, IPolygonClient
    {
        private readonly IPolygonApi polygonApi;
        private const int GasLimit = 21000;
        private static Precision Precision { get; } = Precision.Gwei;
        internal PolygonClient()
        {
        }

        internal PolygonClient(string apiBaseUrl, string xApiKey)
        {
            polygonApi = RestClientFactory.Create<IPolygonApi>(apiBaseUrl, xApiKey);
        }

        public static IPolygonClient Create(string apiBaseUrl, string xApiKey)
        {
            return new PolygonClient(apiBaseUrl, xApiKey);
        }

        Task<EthereumAccountBalance> IPolygonClient.GetAccountBalance(string address)
        {
            return polygonApi.GetBalance(address);
        }

        Task<Signature> IPolygonClient.SendTransactionKMS(TransferPolygonBlockchainKMS transfer)
        {
            return polygonApi.SendTransactionKMS(transfer);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var balance = await polygonApi.GetBalance(request.Address);
            return TatumHelper.ToDecimal(balance.Balance);
        }

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            //var gasPrice = (TatumHelper.ToLong(transfer.Fee, Precision) / GasLimit).ToString();
            var req = new TransferPolygonBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                Amount = transfer.Amount.ToString(),
                Currency = Currency,
                Data = transfer.Message,
                Index = transfer.Index,
                To = transfer.ToAddress,
                //Fee = new Fee()
                //{
                //    GasLimit = GasLimit.ToString(),
                //    GasPrice = gasPrice
                //}
            };
            var tx = await polygonApi.SendTransactionKMS(req);
            return tx;
        }

        public override async Task<decimal> GetTransactionFee(string transactionHash)
        {
            var tx = await polygonApi.GetTransaction(transactionHash);
            return tx.Fee;
        }

        public async Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            var address = await polygonApi.GenerateAddress(xPubString, index);
            return new GenerateAddressResponse() { Address = address.Address, BlockchainAddressType = BlockchainAddressType.ReceiveAddress };
        }
    }
}
