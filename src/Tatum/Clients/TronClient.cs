﻿using System;
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
    }
}