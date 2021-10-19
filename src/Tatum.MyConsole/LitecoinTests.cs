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
    public class LitecoinTests
    {
        ILitecoinClient litecoinClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            litecoinClient = LitecoinClient.Create(baseUrl, xApiKey);
        }

        public async Task GetBalance()
        {
            var address1 = "mhxuc8xJVx9fh4uwQLHngVY9Le2LmYFevQ";
            var address2 = "tltc1qw8xwcerar6u0p5sp253jn2g38verelx49ctwva";
            var req = new BalanceRequest()
            {
                Address = address1
            };
            var response = await litecoinClient.GetBalance(req);
        }

        public async Task GenerateAddress()
        {
            string xPub = "ttub4f491UVVMvWRPLGE7HqAZv2BU5YuDxj1pVbHdi1r5Nx4RCTzu6S8awMvWzbbMEU7zmWiFuD8iuYbVrd8iWJDybRdt4dBYYr2Exrw5vVTTzn";
            int index = 1;
            var add = await litecoinClient.GenerateAddress(xPub, index);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "mhxuc8xJVx9fh4uwQLHngVY9Le2LmYFevQ";
            var address2 = "n28hDC8fB1LLmk5a43SsNgYpZ6hx74cKbx";
            var signatureId = "620ceb86-e2ca-47a9-8e52-b9d8a74af8e0";
            var r = await litecoinClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = address1,
                    Amount = 0.002M,
                    ToAddress = address2,
                    SignatureId = signatureId
                });
        }
    }
}
