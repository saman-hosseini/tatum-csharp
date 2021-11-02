using System;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class BscClient : BaseClient, IBscClient
    {
        private readonly IBscApi bscApi;
        private const int GasLimit = 21000;
        private static Precision Precision { get; } = Precision.Gwei;
        private const string CoinName = "BNB";
        private const string ChainName = "BSC";
        internal BscClient()
        {
        }

        internal BscClient(string apiBaseUrl, string xApiKey)
        {
            bscApi = RestClientFactory.Create<IBscApi>(apiBaseUrl, xApiKey);
        }

        public static IBscClient Create(string apiBaseUrl, string xApiKey)
        {
            return new BscClient(apiBaseUrl, xApiKey);
        }

        Task<EthereumAccountBalance> IBscClient.GetAccountBalance(string address)
        {
            return bscApi.GetAccountBalance(address);
        }

        Task<Signature> IBscClient.SendTransactionKMS(TransferBscBlockchainKMS transfer)
        {
            return bscApi.SendTransactionKMS(transfer);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            EthereumAccountBalance balance;
            if (Currency == CoinName)
            {
                balance = await bscApi.GetAccountBalance(request.Address);
                return TatumHelper.ToDecimal(balance.Balance);
            }
            balance = await bscApi.GetBalance(request.Address, Currency, ContractAddress);
            return TatumHelper.ToDecimal(TatumHelper.ToFormat(balance.Balance, DecimalPrecision));
        }

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            if (Currency == CoinName)
            {
                var fee = await bscApi.EstimateFee(new EthereumEstimateFee()
                {
                    From = transfer.FromAddress,
                    To = transfer.ToAddress,
                    Amount = transfer.Amount.ToString(),
                    Data = transfer.Message
                });
                //fee.GasPrice = TatumHelper.ToFormat(fee.GasPrice, 9);
                var req = new TransferBscBlockchainKMS()
                {
                    SignatureId = transfer.SignatureId,
                    Amount = transfer.Amount.ToString(),
                    Currency = Currency,
                    //Fee = fee,
                    To = transfer.ToAddress,
                    Index = transfer.Index,
                    Data = transfer.Message
                };
                var tx = await bscApi.SendTransactionKMS(req);
                return tx;
            }
            else
            {
                var req = new TransferBscTokenBlockchainKMS()
                {
                    Chain = ChainName,
                    SignatureId = transfer.SignatureId,
                    Amount = transfer.Amount.ToString(),
                    ContractAddress = ContractAddress,
                    To = transfer.ToAddress,
                    Index = transfer.Index,
                    Digits = DecimalPrecision
                };
                var tx = await bscApi.SendTokenTransactionKMS(req);
                return tx;
            }
        }

        public override async Task<decimal> GetTransactionFee(string transactionHash)
        {
            var tx = await bscApi.GetTransaction(transactionHash);
            return tx.Fee;
        }

        public async Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            var address = await bscApi.GenerateAddress(xPubString, index);
            return new GenerateAddressResponse() { Address = address.Address, BlockchainAddressType = BlockchainAddressType.ReceiveAddress };
        }
    }
}
