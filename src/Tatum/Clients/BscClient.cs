﻿using System;
using System.Threading.Tasks;
using Tatum.Blockchain;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Clients
{
    public partial class BscClient : IBscClient
    {
        private readonly IBscApi bscApi;

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

        Task<TransactionHash> IBscClient.SendTransactionKMS(TransferBscBlockchainKMS transfer)
        {
            return bscApi.SendTransactionKMS(transfer);
        }
    }
}
