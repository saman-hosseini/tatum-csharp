﻿using Nethereum.Web3;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class EthereumClient : BaseClient, IEthereumClient
    {
        private readonly IEthereumApi ethereumApi;
        private readonly IEthereumGasApi ethereumGasApi;
        private readonly string tatumWeb3DriverUrl;
        private const int GasLimit = 21000;
        /// <summary>
        /// Ethereum actual decimal precision is 18
        /// </summary>
        private static Precision Precision { get; } = Precision.Gwei;
        private const string CoinName = "ETH";
        private const string ChainName = "ETH";
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

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            if (Currency == CoinName)
            {
                var fee = await ethereumApi.EstimateFee(new EthereumEstimateFee()
                {
                    From = transfer.FromAddress,
                    To = transfer.ToAddress,
                    Amount = transfer.Amount.ToString(),
                    Data = transfer.Message
                });
                fee.GasPrice = TatumHelper.ToFormat(fee.GasPrice, 9);
                var sendObj = new TransferEthereumErc20KMS()
                {
                    SignatureId = transfer.SignatureId,
                    Amount = transfer.Amount.ToString(),
                    Currency = Currency,
                    To = transfer.ToAddress,
                    Fee = fee,
                    Index = transfer.Index,
                    Data = transfer.Message
                };
                var tx = await ethereumApi.SendTransactionKMS(sendObj);
                return tx;
            }
            else
            {
                var sendObj = new TransferEthereumTokenErc20KMS()
                {
                    Chain = ChainName,
                    SignatureId = transfer.SignatureId,
                    Amount = transfer.Amount.ToString(),
                    To = transfer.ToAddress,
                    Fee = new Fee()
                    {
                        GasLimit = GasLimit.ToString(),
                        GasPrice = "40"
                    },
                    ContractAddress = ContractAddress,
                    Digits = DecimalPrecision,
                    Index = transfer.Index,
                    
                };
                var tx = await ethereumApi.SendTokenTransactionKMS(sendObj);
                return tx;
            }
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            if (Currency == CoinName)
            {
                var balance = await ethereumApi.GetAccountBalance(request.Address);
                return TatumHelper.ToDecimal(balance.Balance);
            }
            else
            {
                var balance = await ethereumApi.GetErc20AccountBalance(request.Address, Currency, ContractAddress);
                return TatumHelper.ToDecimal(balance.Balance);
            }
        }

        public override async Task<decimal> GetTransactionFee(string transactionHash)
        {
            var tx = await ethereumApi.GetTransaction(transactionHash);
            return tx.Fee;
        }

        public async Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            var address = await ethereumApi.GenerateAddress(xPubString, index);
            return new GenerateAddressResponse() { Address = address.Address, BlockchainAddressType = BlockchainAddressType.ReceiveAddress };
        }
    }
}
