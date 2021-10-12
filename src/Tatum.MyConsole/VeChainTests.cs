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
    public class VeChainTests
    {
        IVeChainClient veChainClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            veChainClient = VeChainClient.Create(baseUrl, xApiKey);
        }

        public async Task GetBalance()
        {
            var Maddress = "0xe4bdce3fee7cd2d722580b0e701531bae004b85b";
            var req = new BalanceRequest()
            {
                Address = Maddress
            };
            var response = await veChainClient.GetBalance(req);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "0xe4bdce3fee7cd2d722580b0e701531bae004b85b";
            var address2 = "0x6a03e1FF4b617bb37fe238AC3b11a91Af68F5112";
            var signatureId = "edd6587e-db00-49a9-9ee3-4c4112ed6b5e";
            var r = await veChainClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = address1,
                    Amount = 5.3M,
                    ToAddress = address2,
                    Index = 1,
                    SignatureId = signatureId
                });
        }

        public async Task GenerateAddress()
        {
            string xPub = "xpub6FGg7CoGebEDo4PX3UuNi4chj7YoPWZFZeZdCFwq8YcHcBQUnkqrTD9g3qCgZSWsMKa5HiA7YiNA9hWkAXBt8bwsy8mGwinLh6DMN4iqoHU";
            int index = 1;
            var add = await veChainClient.GenerateAddress(xPub, index);
        }

        public async Task FindIndexAddress()
        {
            string xPub = "xpub6F2SjKKrCbV67QmKBp9rZFYx151uR2ntkShivQXeU7eB2wvrLpA4tjaCGAP8ELWdQUBRSVeQcp2VKY5fpTknTEfL7kqTjEdwsFups1o9Q68";
            var address = "0x6a03e1FF4b617bb37fe238AC3b11a91Af68F5112";
            for (int i = 230; i < int.MaxValue; i++)
            {
                var add = await veChainClient.GenerateAddress(xPub, i);
                Console.WriteLine(i);
                await Task.Delay(100);
                if (add.Address == address)
                {

                }
            }
        }
    }
}
