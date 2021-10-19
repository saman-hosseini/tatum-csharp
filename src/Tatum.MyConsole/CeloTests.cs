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
    public class CeloTests
    {
        ICeloClient celoClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            celoClient = CeloClient.Create(baseUrl, xApiKey);
            celoClient.Currency = "CELO";
        }

        public async Task GetBalance()
        {
            var address = "0x307eaba8b2c0f756d64d7ee704b9e88954fca8a9";
            var req = new BalanceRequest()
            {
                Address = address
            };
            var response = await celoClient.GetBalance(req);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "0x307eaba8b2c0f756d64d7ee704b9e88954fca8a9";
            var address2 = "0xbd76e88a1abf05d1c49803dab874841570570ea9";
            var signatureId = "3021a27c-ccc1-4ad5-bc8d-5f7d7315c591";
            var r = await celoClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = address1,
                    Amount = .02M,
                    Index = 1,
                    ToAddress = address2,
                    SignatureId = signatureId
                });
        }
    }
}
