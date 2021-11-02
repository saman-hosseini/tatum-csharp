using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class TronClient : BaseClient, ITronClient
    {
        private readonly ITronApi tronApi;
        private const string CoinName = "TRON";
        private const string TRC10 = "TRC10";
        private const string TRC20 = "TRC20";
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

        Task<Signature> ITronClient.SendTransactionKMS(TransferTronBlockchainKMS transfer)
        {
            return tronApi.SendTransactionKMS(transfer);
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

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            if (Currency == CoinName)
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
            else if (ContractType == TRC20)
            {
                var tx = await tronApi.SendTrc20TransactionKMS(new TransferTronTrc20BlockchainKMS()
                {
                    SignatureId = transfer.SignatureId,
                    From = transfer.FromAddress,
                    To = transfer.ToAddress,
                    Amount = transfer.Amount.ToString(),
                    Index = transfer.Index
                });
                return tx;
            }
            else if (ContractType == TRC10)
            {
                var tx = await tronApi.SendTrc10TransactionKMS(new TransferTronTrc10BlockchainKMS()
                {
                    SignatureId = transfer.SignatureId,
                    From = transfer.FromAddress,
                    To = transfer.ToAddress,
                    Amount = transfer.Amount.ToString(),
                    Index = transfer.Index
                });
                return tx;
            }
            throw new NotImplementedException();
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var account = await tronApi.GetAccount(request.Address);

            if (Currency == CoinName)
            {
                return TatumHelper.ToDecimal(account.Balance, Precision);
            }
            else if (ContractType == TRC10)
            {
                return TatumHelper.ToDecimal(account.Trc10.FirstOrDefault(q => q.Key == ContractAddress).Value, DecimalPrecision);
            }
            else if (ContractType == TRC20)
            {
                string balance = "0";
                account.Trc20.FirstOrDefault(q => q.TryGetValue(ContractAddress, out balance));
                return TatumHelper.ToDecimal(TatumHelper.ToLong(balance), DecimalPrecision);
            }
            throw new TatumException($"{ContractType} cant be found in Tron", System.Net.HttpStatusCode.BadRequest);
        }

        public override async Task<decimal> GetTransactionFee(string transactionHash)
        {
            var tx = await tronApi.GetTransaction(transactionHash);
            var fee = TatumHelper.ToDecimal(tx.NetFee + tx.EnergyFee, Precision);
            return fee;
        }

        public async Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            var address = await tronApi.GenerateAddress(xPubString, index);
            return new GenerateAddressResponse() { Address = address.Address, BlockchainAddressType = BlockchainAddressType.ReceiveAddress };
        }
    }
}
