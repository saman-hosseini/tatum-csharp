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
            var req = new BalanceRequest()
            {
                Address = address
            };
            var response = await bscClient.GetBalance(req);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "0xe4bdce3fee7cd2d722580b0e701531bae004b85b";
            var address2 = "0x3cc789313fbe6b482a871cec004261759c0699f4";
            var req = new TransferBlockchainKMS()
            {
                FromAddress = address1,
                ToAddress = address2,
                Amount = 0.0042M,
                Currency = "ETH",
                Fee = 0.001M,
                SignatureId = "3be381a1-d149-4f86-9c58-b4626b0f502f"
            };
            var response = await bscClient.SendTransactionKMS(req);
        }
    }
}
