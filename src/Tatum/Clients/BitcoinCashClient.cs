using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class BitcoinCashClient : BaseClient, IBitcoinCashClient
    {
        private readonly IBitcoinCashApi bitcoinCashApi;

        internal BitcoinCashClient()
        {
        }

        internal BitcoinCashClient(string apiBaseUrl, string xApiKey)
        {
            bitcoinCashApi = RestClientFactory.Create<IBitcoinCashApi>(apiBaseUrl, xApiKey);
        }

        public static IBitcoinCashClient Create(string apiBaseUrl, string xApiKey)
        {
            return new BitcoinCashClient(apiBaseUrl, xApiKey);
        }

        Task<TransactionHash> IBitcoinCashClient.BroadcastSignedTransaction(BroadcastRequest request)
        {
            var validationContext = new ValidationContext(request);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);

            return bitcoinCashApi.BroadcastSignedTransaction(request);
        }

        Task<BitcoinCashBlock> IBitcoinCashClient.GetBlock(string hash)
        {
            return bitcoinCashApi.GetBlock(hash);
        }

        Task<BitcoinCashInfo> IBitcoinCashClient.GetBlockchainInfo()
        {
            return bitcoinCashApi.GetBlockchainInfo();
        }

        Task<BlockHash> IBitcoinCashClient.GetBlockHash(long blockHeight)
        {
            return bitcoinCashApi.GetBlockHash(blockHeight);
        }

        Task<BitcoinCashTx> IBitcoinCashClient.GetTransaction(string hash)
        {
            return bitcoinCashApi.GetTransaction(hash);
        }

        Task<List<BitcoinCashTx>> IBitcoinCashClient.GetTxForAccount(string address, int skip)
        {
            return bitcoinCashApi.GetTxForAccount(address, skip);
        }

        Task<Signature> IBaseClient.SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            throw new System.NotImplementedException();
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            var address = await bitcoinCashApi.GenerateAddress(xPubString, index);
            return new GenerateAddressResponse() { Address = address.Address, BlockchainAddressType = BlockchainAddressType.ReceiveAddress };
        }
    }
}
