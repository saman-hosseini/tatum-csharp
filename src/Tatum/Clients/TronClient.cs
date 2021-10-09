using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class TronClient : ITronClient
    {
        private readonly ITronApi tronApi;
        private const string CoinName = "TRON";
        private static Precision Precision { get; } = Precision.Precision6;
        internal TronClient()
        {
        }

        internal TronClient(string apiBaseUrl, string xApiKey)
        {
            tronApi = RestClientFactory.Create<ITronApi>(apiBaseUrl, xApiKey);
        }

        public static ITronClient Create(string apiBaseUrl, string xApiKey)
        {
            return new TronClient(apiBaseUrl, xApiKey);
        }

        Task<TransactionHash> ITronClient.Broadcast(BroadcastRequest request)
        {
            var validationContext = new ValidationContext(request);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);

            return tronApi.BroadcastSignedTransaction(request);
        }

        Task<TronInfo> ITronClient.GetBlockchainInfo()
        {
            return tronApi.GetBlockchainInfo();
        }

        Task<TronBlock> ITronClient.GetBlock(string hash)
        {
            return tronApi.GetBlock(hash);
        }

        Task<TronAccount> ITronClient.GetAccount(string address)
        {
            return tronApi.GetAccount(address);
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            if (transfer.Currency == CoinName)
            {
                var tx = await tronApi.SendTransactionKMS(new TransferTronBlockchainKMS()
                {
                    SignatureId = transfer.SignatureId,
                    From = transfer.FromAddress,
                    To = transfer.ToAddress,
                    Amount = transfer.Amount.ToString(),
                    Index = transfer.Index
                });
                return tx;
            }
            else
            {
                var tx = await tronApi.SendTransactionKMS(new TransferTronBlockchainKMS()
                {
                    SignatureId = transfer.SignatureId,
                    From = transfer.FromAddress,
                    To = transfer.ToAddress,
                    Amount = transfer.Amount.ToString(),
                    Index = transfer.Index
                });
                return tx;
            }
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var account = await tronApi.GetAccount(request.Address);
            if (request.Currency == CoinName)
            {
                return TatumHelper.ToDecimal(account.Balance, Precision);
            }
            else
            {
                return 0M;
            }
        }
    }
}
