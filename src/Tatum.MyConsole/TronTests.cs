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
        IBscClient bscClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            bscClient = BscClient.Create(baseUrl, xApiKey);
            tronClient = TronClient.Create(baseUrl, xApiKey);
        }

        public async Task EndpointTest()
        {
            var response = await tronClient.GetBlockchainInfo();
        }

        public void CreateWalletMainNet()
        {
            Wallet wallet = tronClient.CreateWallet(mnemonic, false);
        }

        public void CreateWalletTestNet()
        {
            Wallet wallet = tronClient.CreateWallet(mnemonic, true);
        }

        public void GeneratePrivateKeyMainNet()
        {
            var privateKey = tronClient.GeneratePrivateKey(mnemonic, 1, false);
        }

        public void GeneratePrivateKeyTestNet()
        {
            var privateKey = tronClient.GeneratePrivateKey(mnemonic, 1, true);
        }

        public async Task GenerateAddressMainNet()
        {
            var address = await tronClient.GenerateAddress("033dd961ca356b6c9b0af052781895d564b22b3650decb9f5bc218a75a9b5dc007b36f9250ff2eb91359d307032c358c6e3c22f2a793f2dbf8196f8ff1ead35af4", 1);
        }

        public async Task GenerateAddress()
        {
            string xPub = "033dd961ca356b6c9b0af052781895d564b22b3650decb9f5bc218a75a9b5dc007b36f9250ff2eb91359d307032c358c6e3c22f2a793f2dbf8196f8ff1ead35af4";
            int index = 1;
            var add = await tronClient.GenerateAddress(xPub, index);
        }

        public async Task GetAccount()
        {
            string adress = "TDBndvRdCGNi1cCpLK3zYenv6rjhftPvts";
            var account = await tronClient.GetAccount(adress);
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
            var r = await tronClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = address1,
                    Amount = 50,
                    Fee = 0,
                    ToAddress = address2,
                    SignatureId = signatureId,
                    Index = 1,
                    Currency = "TRON"
                });
        }
    }
}
