using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatum.Clients;
using Tatum.Model.Requests;

namespace Tatum.MyConsole
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

        public void CreateWalletMainNet()
        {
            string expectedXPub = "033dd961ca356b6c9b0af052781895d564b22b3650decb9f5bc218a75a9b5dc007b36f9250ff2eb91359d307032c358c6e3c22f2a793f2dbf8196f8ff1ead35af4";
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

        public void GenerateAddressMainNet()
        {
            string expectedAdress = "TKpy7kpXbF8P6gxEXaTbyfAZGjNyCeTAre";
            string address = tronClient.GenerateAddress("033dd961ca356b6c9b0af052781895d564b22b3650decb9f5bc218a75a9b5dc007b36f9250ff2eb91359d307032c358c6e3c22f2a793f2dbf8196f8ff1ead35af4", 1, false);
        }

        public async Task GetAccount()
        {
            string adress = "TKpy7kpXbF8P6gxEXaTbyfAZGjNyCeTAre";
            var account = await tronClient.GetTronAccount(adress);
        }

        public async Task SendTransactionKMS()
        {
            var body = new TransferTronBlockchainKMS()
            {
                From = "TDBndvRdCGNi1cCpLK3zYenv6rjhftPvts",
                SignatureId = "79981416-4e03-452f-bda5-a869fb955376",
                To = "TC5kLNd3A7fnxJfYSbUs99F4Qh4C1K47HV",
                Amount = "50.9",
                Index = 4
            };
            var hash = await tronClient.SendTransactionKMS(body);
        }

        public void FromAddressAndFromUtxoNotTogether()
        {
            var body = new TransferBtcBasedBlockchain
            {
                FromAddresses = new List<FromAddress>
                {
                    new FromAddress
                    {
                        Address = "2MzNGwuKvMEvKMQogtgzSqJcH2UW3Tc5oc7",
                        PrivateKey = "cVX7YtgL5muLTPncHFhP95oitV1mqUUA5VeSn8HeCRJbPqipzobf"
                    }
                },
                FromUtxos = new List<FromUtxo>
                {
                    new FromUtxo
                    {
                        TxHash = "53faa103e8217e1520f5149a4e8c84aeb58e55bdab11164a95e69a8ca50f8fcc",
                        Index = 0,
                        PrivateKey = "cVX7YtgL5muLTPncHFhP95oitV1mqUUA5VeSn8HeCRJbPqipzobf"
                    }
                },
                Tos = new List<To>
                {
                    new To
                    {
                        Address = "2MzNGwuKvMEvKMQogtgzSqJcH2UW3Tc5oc7",
                        Value = 0.02969944M
                    }
                }
            };

            var validationContext = new ValidationContext(body);

        }
    }
}
