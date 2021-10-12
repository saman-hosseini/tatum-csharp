using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Clients;
using TatumPlatform.Model.Requests;

namespace TatumPlatform.MyConsole
{
    public class BitcoinCashTests
    {
        IBitcoinCashClient bitcoinCashClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            bitcoinCashClient = BitcoinCashClient.Create(baseUrl, xApiKey);
        }

        public async Task GetBalance()
        {
            var Maddress = "bchtest:qz5q7l34dsnstcd4m7qe6099wx7j5tu2zcf6d4ez7p";
            var req = new BalanceRequest()
            {
                Address = Maddress
            };
            var response = await bitcoinCashClient.GetBalance(req);
        }

        public async Task GenerateAddress()
        {
            string xPub = "tpubDEx6jhQE6o1MmaHpXmugZHBXNYLJdqeABiD5SFfFfchb2Xwfr2RdG36mSNMqYjSccKXaVtc4thze3jtbhpHyggbCEUj5YHypDi79BaeYVae";
            int index = 2;
            var add = await bitcoinCashClient.GenerateAddress(xPub, index);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "bchtest:qz5q7l34dsnstcd4m7qe6099wx7j5tu2zcf6d4ez7p";
            var address2 = "rNopBjEd3Ck94VE7to3X26VQL98F5BtGCr";
            var signatureId = "debe7373-303e-40e5-aed6-b6cb78cb191f";
            var r = await bitcoinCashClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = address1,
                    Amount = 125,
                    Fee = 1,
                    FromTag = 3,
                    ToTag = 2,
                    ToAddress = address2,
                    SignatureId = signatureId
                });
        }
    }
}
