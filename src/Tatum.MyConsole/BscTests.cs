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
    public class BscTests
    {
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
        }

        public async Task GetBalance()
        {
            var address = "0xe4bdce3fee7cd2d722580b0e701531bae004b85b";
            var addressMain = "0x477AEf00A114a5A2506218E9851D8618b9Fc6B76";
            var req = new BalanceRequest()
            {
                Address = address
            };
            var response = await bscClient.GetBalance(req);
        }

        public async Task GenerateAddress()
        {
            string xPub = "xpub6FGg7CoGebEDo4PX3UuNi4chj7YoPWZFZeZdCFwq8YcHcBQUnkqrTD9g3qCgZSWsMKa5HiA7YiNA9hWkAXBt8bwsy8mGwinLh6DMN4iqoHU";
            int index = 2;
            var add = await bscClient.GenerateAddress(xPub, index);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "0xe4bdce3fee7cd2d722580b0e701531bae004b85b";
            var address2 = "0x3cc789313fbe6b482a871cec004261759c0699f4";
            var signatureId = "35521f03-310c-4544-8732-54cfcd2138e0";
            var req = new TransferBlockchainKMS()
            {
                FromAddress = address1,
                ToAddress = address2,
                Amount = 0.0043M,
                Currency = "BSC",
                Fee = 0.0022M,
                Index = 1,
                SignatureId = signatureId
            };
            var response = await bscClient.SendTransactionKMS(req);
        }
    }
}
