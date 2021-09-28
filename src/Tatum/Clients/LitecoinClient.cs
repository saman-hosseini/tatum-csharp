﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    /// <inheritdoc/>
    public partial class LitecoinClient : ILitecoinClient
    {
        private readonly ILitecoinApi litecoinApi;

        internal LitecoinClient()
        {
        }

        internal LitecoinClient(string apiBaseUrl, string xApiKey)
        {
            litecoinApi = RestClientFactory.Create<ILitecoinApi>(apiBaseUrl, xApiKey);
        }

        public static ILitecoinClient Create(string apiBaseUrl, string xApiKey)
        {
            return new LitecoinClient(apiBaseUrl, xApiKey);
        }

        Task<TransactionHash> ILitecoinClient.BroadcastSignedTransaction(BroadcastRequest request)
        {
            var validationContext = new ValidationContext(request);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);

            return litecoinApi.BroadcastSignedTransaction(request);
        }

        Task<LitecoinBlock> ILitecoinClient.GetBlock(string hash)
        {
            return litecoinApi.GetBlock(hash);
        }

        Task<LitecoinInfo> ILitecoinClient.GetBlockchainInfo()
        {
            return litecoinApi.GetBlockchainInfo();
        }

        Task<BlockHash> ILitecoinClient.GetBlockHash(long blockHeight)
        {
            return litecoinApi.GetBlockHash(blockHeight);
        }

        Task<LitecoinTx> ILitecoinClient.GetTransaction(string hash)
        {
            return litecoinApi.GetTransaction(hash);
        }

        Task<List<LitecoinTx>> ILitecoinClient.GetTxForAccount(string address, int pageSize, int offset)
        {
            return litecoinApi.GetTxForAccount(address, pageSize, offset);
        }

        Task<LitecoinUtxo> ILitecoinClient.GetUtxo(string txHash, int txOutputIndex)
        {
            return litecoinApi.GetUtxo(txHash, txOutputIndex);
        }

        Task<TransactionHash> ILitecoinClient.SendTransactionKMS(TransferBtcBasedBlockchainKMS transferBtc)
        {
            return litecoinApi.SendTransactionKMS(transferBtc);
        }

        Task<BitcoinAccountBalance> ILitecoinClient.GetAccountBalance(string address)
        {
            return litecoinApi.GetAccountBalance(address);
        }
    }
}
