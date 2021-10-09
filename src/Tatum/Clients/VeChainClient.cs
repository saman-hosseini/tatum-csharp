using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;
using TatumPlatform.Model.Rules;

namespace TatumPlatform.Clients
{
    public partial class VeChainClient : IVeChainClient
    {
        private readonly IVeChainApi veChainApi;
        private const int GasLimit = 21000;
        private static Precision Precision { get; } = Precision.Precision18;
        internal VeChainClient()
        {
        }

        internal VeChainClient(string apiBaseUrl, string xApiKey)
        {
            veChainApi = RestClientFactory.Create<IVeChainApi>(apiBaseUrl, xApiKey);
        }

        public static IVeChainClient Create(string apiBaseUrl, string xApiKey)
        {
            return new VeChainClient(apiBaseUrl, xApiKey);
        }

        Task<TransactionHash> IVeChainClient.Broadcast(BroadcastRequest request)
        {
            var validationContext = new ValidationContext(request);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);

            return veChainApi.Broadcast(request);
        }

        Task<long> IVeChainClient.EstimateGas(EstimateGasRequest request)
        {
            var validationContext = new ValidationContext(request);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);

            return veChainApi.EstimateGas(request);
        }

        Task<VeChainAccountBalance> IVeChainClient.GetAccountBalance(string address)
        {
            return veChainApi.GetBalance(address);
        }

        Task<VeChainAccountEnergy> IVeChainClient.GetAccountEnergy(string address)
        {
            return veChainApi.GetAccountEnergy(address);
        }

        Task<VeChainBlock> IVeChainClient.GetBlock(string hash)
        {
            return veChainApi.GetBlock(hash);
        }

        Task<long> IVeChainClient.GetCurrentBlock()
        {
            return veChainApi.GetCurrentBlock();
        }

        Task<VeChainTx> IVeChainClient.GetTransaction(string hash)
        {
            return veChainApi.GetTransaction(hash);
        }

        Task<VeChainTxReceipt> IVeChainClient.GetTransactionReceipt(string hash)
        {
            return veChainApi.GetTransactionReceipt(hash);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var balance = await veChainApi.GetBalance(request.Address);
            return TatumHelper.ToDecimal(balance.Balance);
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var validator = new VeChainTransferValidator();
            var validation = validator.Validate(transfer);

            var req = new TransferVeChainBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                Amount = transfer.Amount.ToString(),
                Data = transfer.Message,
                To = transfer.ToAddress,
                Fee = new VeChainFee()
                {
                    GasLimit = GasLimit.ToString()
                }
            };
            var tx = await veChainApi.SendTransactionKMS(req);
            return tx;
        }

        public async Task<string> GenerateAddress(string xPubString, int index)
        {
            var address = await veChainApi.GenerateAddress(xPubString, index);
            return address.Address;
        }
    }
}
