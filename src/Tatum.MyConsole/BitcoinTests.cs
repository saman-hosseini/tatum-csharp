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
    public class BitcoinTests
    {
        IBitcoinClient bitcoinClient;
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

            bitcoinClient = BitcoinClient.Create(baseUrl, xApiKey);
        }

        public async Task EndpointTest()
        {
            var response = await bitcoinClient.GetBlockchainInfo();
        }

        public void CreateWalletMainNet()
        {
            string expectedXPub = "xpub6ErBxoAXLaRuyMrByGBZYnPWbh7R7EMgd74wBnK8X2iR2vShuEAZFkhpSoT5tCY1eNK2jcspa7ijnDD3u1CRgUVNEtjmSkaLtxAPqWBBenz";
            Wallet wallet = bitcoinClient.CreateWallet(mnemonic, false);
        }

        public void CreateWalletTestNet()
        {
            Wallet wallet = bitcoinClient.CreateWallet(mnemonic, true);
        }

        public void GeneratePrivateKeyMainNet()
        {
            var privateKey = bitcoinClient.GeneratePrivateKey(mnemonic, 1, false);
        }

        public void GeneratePrivateKeyTestNet()
        {
            var privateKey = bitcoinClient.GeneratePrivateKey(mnemonic, 1, true);
        }

        public void GenerateAddressMainNet()
        {
            string address = bitcoinClient.GenerateAddress("xpub6EsCk1uU6cJzqvP9CdsTiJwT2rF748YkPnhv5Qo8q44DG7nn2vbyt48YRsNSUYS44jFCW9gwvD9kLQu9AuqXpTpM1c5hgg9PsuBLdeNncid", 1, false);

        }

        public void GenerateAddressTestNet()
        {
            string address = bitcoinClient.GenerateAddress("tpubDFjLw3ykn4aB7fFt96FaqRjSnvtDsU2wpVr8GQk3Eo612LS9jo9JgMkQRfYVG248J3pTBsxGg3PYUXFd7pReNLTeUzxFcUDL3zCvrp3H34a", 1, true);

        }

        public async Task TransactionFromUtxo()
        {
            var body = new TransferBtcBasedBlockchain
            {
                FromUtxos = new List<FromUtxo>
                {
                    new FromUtxo
                    {
                        TxHash = "060d1bde52949044971b056aaec807e1189ca80db4d06e90d4f312a610de2aee",
                        Index = 0,
                        PrivateKey = "cSmnhYYG2mXRPvi1FoFDihT4bL5qy9DDhephoubJ7mwxb2sgLNGQ"
                    }
                },
                Tos = new List<To>
                {
                    new To
                    {
                        Address = "mkQporSV7myJLwfWEVyHMhphY9viiiEMWc",
                        Value = 0.00005m
                    }
                }
            };

            var txData = await bitcoinClient.PrepareSignedTransaction(body, true);
        }

        public async Task TransactionFromAddress()
        {
            var body = new TransferBtcBasedBlockchain
            {
                FromAddresses = new List<FromAddress>
                {
                    new FromAddress
                    {
                        Address = "mfk4BVNg4p4m7qPx3u398otHT97M9hotPR",
                        PrivateKey = "cSmnhYYG2mXRPvi1FoFDihT4bL5qy9DDhephoubJ7mwxb2sgLNGQ"
                    }
                },
                Tos = new List<To>
                {
                    new To
                    {
                        Address = "mkQporSV7myJLwfWEVyHMhphY9viiiEMWc",
                        Value = 0.00005m
                    }
                }
            };

            var txData = await bitcoinClient.PrepareSignedTransaction(body, true);
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
