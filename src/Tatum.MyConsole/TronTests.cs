using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Clients;
using TatumPlatform.Model.Requests;

namespace TatumPlatform.MyConsole
{
    public class TronTests
    {
        ITronClient tronClient;
        //https://github.com/bitcoin/bips/blob/master/bip-0039/english.txt
        readonly string mnemonic = "divert bread quantum tobacco key they one leaf good forward erupt split kitten maid mean crime youth chief jungle mind design broken tilt bus";
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            tronClient = TronClient.Create(baseUrl, xApiKey);
        }

        public async Task EndpointTest()
        {
            var response = await tronClient.GetBlockchainInfo();
        }

        public async Task GenerateAddress()
        {
            string xPub = "033dd961ca356b6c9b0af052781895d564b22b3650decb9f5bc218a75a9b5dc007b36f9250ff2eb91359d307032c358c6e3c22f2a793f2dbf8196f8ff1ead35af4";
            int index = 1000;
            var add = await tronClient.GenerateAddress(xPub, index);
        }

        public async Task GetAccount()
        {
            string adress = "TDBndvRdCGNi1cCpLK3zYenv6rjhftPvts";
            string adress2 = "TMCBHqyAbFDG1mbu64YABMLRMwGef9u9to";
            var account = await tronClient.GetAccount(adress);
        }

        public async Task GetBalance()
        {
            tronClient.ContractAddress = "1003533";
            tronClient.Currency = "PAL";
            tronClient.ContractType = "TRC10";
            tronClient.DecimalPrecision = 2;
            var Maddress = "TDBndvRdCGNi1cCpLK3zYenv6rjhftPvts";
            var req = new BalanceRequest()
            {
                Address = Maddress,
            };
            var response = await tronClient.GetBalance(req);
        }

        public async Task SendTransaction()
        {
            var transfer = new TransferTronBlockchainKMS()
            {
                From = "TDBndvRdCGNi1cCpLK3zYenv6rjhftPvts",
                SignatureId = "79981416-4e03-452f-bda5-a869fb955376",
                To = "TC5kLNd3A7fnxJfYSbUs99F4Qh4C1K47HV",
                Amount = "50.9",
                Index = 1
            };
            var hash = await tronClient.SendTransactionKMS(transfer);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "TUr5jyXgdCvpUfBMa9gqSekdTdFFGzZ9wD";
            var address2 = "TDqxs8V9QdQ7NHGZgoR9ParThTVkSgs6Ro";
            var signatureId = "8df80a24-378c-4c9b-8080-023ee3e03ad4";
            tronClient.Currency = "TRON";


            var r = await tronClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = address1,
                    Amount = 2.3M,
                    ToAddress = address2,
                    SignatureId = signatureId,
                    Index = 1
                });
        }

        public async Task GetTransactionFee()
        {
            var hashTest = "c353ed7f0636c7540483c148de4d0f8deb0a201ec6ae4d176032c67d51d25c00";
            var hashTest2 = "462b66eb8fc5e8d63c9fb6ba87e2ce68bf7c066899b9df05c345a3c622cede52";
            var hashMain = "0f465409bb184acad8e6fb6ecbde5514851fa43899cec3f4e35babcfa3f49472";
            var hashMain2 = "6ecae51fce67d746f198b773cc610583da6175b0d891c17505dd1e285140aed2";
            var fee = await tronClient.GetTransactionFee(hashTest2);
        }

        public async Task GetAccountTransactions()
        {
            var address = "TFysCB929XGezbnyumoFScyevjDggu3BPq";
            await tronClient.GetAccountTransactions(address);
        }
    }
}
