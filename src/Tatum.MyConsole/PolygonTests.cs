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
    public class PolygonTests
    {
        IPolygonClient polygonClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            polygonClient = PolygonClient.Create(baseUrl, xApiKey);
            polygonClient.Currency = "MATIC";
        }

        public async Task GetBalance()
        {
            var Maddress = "0xE4bdCe3fEe7Cd2d722580b0e701531BAe004B85B";
            var req = new BalanceRequest()
            {
                Address = Maddress
            };
            var response = await polygonClient.GetBalance(req);
        }

        public async Task GenerateAddress()
        {
            string xPub = "xpub6ELHEaGoiMdiu6u3DGp9725SA5n4imZPemCxG2zHjUxZzF9FFeMKanv4x9d16FcQNeLzRozkLfeEChsSZKnnPvBtL19GfDvR6Mi9vWMQac3";
            int index = 2;
            var add = await polygonClient.GenerateAddress(xPub, index);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "0x3986452b72e1e6a75308abca73003478dd3bc1c4";
            var address2 = "0xbe1d787ad368d27f2853eaec767b70402b1feab7";
            var signatureId = "c1bc7c42-aa3a-4b2d-a463-d90039926034";
            var r = await polygonClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = address1,
                    Amount = 0.0002M,
                    Fee = 0.00066M,
                    Index = 1,
                    ToAddress = address2,
                    SignatureId = signatureId
                });
        }
    }
}
