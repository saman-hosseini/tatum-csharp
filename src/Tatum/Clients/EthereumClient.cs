using Nethereum.Web3;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    /// <inheritdoc/>
    public partial class EthereumClient : IEthereumClient
    {
        private readonly IEthereumApi ethereumApi;
        private readonly IEthereumGasApi ethereumGasApi;
        private readonly string tatumWeb3DriverUrl;
        private const int GasLimit = 21000;

        internal EthereumClient()
        {
        }

        internal EthereumClient(string apiBaseUrl, string xApiKey)
        {
            ethereumApi = RestClientFactory.Create<IEthereumApi>(apiBaseUrl, xApiKey);
            ethereumGasApi = RestClientFactory.Create<IEthereumGasApi>("https://ethgasstation.info/");
            tatumWeb3DriverUrl = $"{apiBaseUrl}/v3/ethereum/web3/{xApiKey}";
        }

        public static IEthereumClient Create(string apiBaseUrl, string xApiKey)
        {
            return new EthereumClient(apiBaseUrl, xApiKey);
        }

        Task<TransactionHash> IEthereumClient.BroadcastSignedTransaction(BroadcastRequest request)
        {
            var validationContext = new ValidationContext(request);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);

            return ethereumApi.BroadcastSignedTransaction(request);
        }

        Task<EthereumAccountBalance> IEthereumClient.GetAccountBalance(string address)
        {
            return ethereumApi.GetAccountBalance(address);
        }

        Task<List<EthereumTx>> IEthereumClient.GetAccountTransactions(string address, int pageSize, int offset)
        {
            return ethereumApi.GetAccountTransactions(address, pageSize, offset);
        }

        Task<EthereumBlock> IEthereumClient.GetBlock(string hash)
        {
            return ethereumApi.GetBlock(hash);
        }

        Task<long> IEthereumClient.GetCurrentBlock()
        {
            return ethereumApi.GetCurrentBlock();
        }

        Task<EthereumAccountBalance> IEthereumClient.GetErc20AccountBalance(string address, string currency, string contractAddress)
        {
            return ethereumApi.GetErc20AccountBalance(address, currency, contractAddress);
        }

        Task<EthereumTx> IEthereumClient.GetTransaction(string hash)
        {
            return ethereumApi.GetTransaction(hash);
        }

        Task<int> IEthereumClient.GetTransactionsCount(string address)
        {
            return ethereumApi.GetTransactionsCount(address);
        }

        private static decimal GweiToEth(long amount)
        {
            return amount / 1000000000M;
        }

        private static long EthToGwei(decimal amount)
        {
            return decimal.ToInt64(amount * 1000000000);
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var gasPrice = (EthToGwei(transfer.Fee) / GasLimit).ToString();
            var sendObj = new TransferEthereumErc20KMS()
            {
                Amount = transfer.Amount.ToString(),
                Currency = transfer.Currency,
                To = transfer.ToAddress,
                Fee = new Fee()
                {
                    GasLimit = GasLimit.ToString(),
                    GasPrice = gasPrice
                },
                SignatureId = transfer.SignatureId
            };
            var tx = await ethereumApi.SendTransactionKMS(sendObj);
            return tx;
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            if (request.Currency == Currency.ETH.ToString())
            {
                var balance = await ethereumApi.GetAccountBalance(request.Address);
                return decimal.Parse(balance.Balance);
            }
            else
            {
                var balance = await ethereumApi.GetErc20AccountBalance(request.Address, request.Currency, request.ContractAddress);
                return decimal.Parse(balance.Balance);
            }
        }
    }
}
