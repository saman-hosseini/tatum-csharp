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
    public class XlmTests
    {
        IXlmClient xlmClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            xlmClient = XlmClient.Create(baseUrl, xApiKey);
            xlmClient.Currency = "XLM";
        }

        public async Task GetBalance()
        {
            var Maddress = "GDDSJUIDT5676C23QOIDP4YN6KK2X7QXCPJDCDPRUIWCP2CXXLRGKUPI";
            string xPub = "GC7Q6ZF35G6MIVLCZYZGOLNLR2KMZMF6HU37QPC2DXQKCABUHNKSRKGP";
            var req = new BalanceRequest()
            {
                Address = xPub,
                
            };
            var response = await xlmClient.GetBalance(req);
        }

        public async Task GenerateAddress()
        {
            string xPub = "GC7Q6ZF35G6MIVLCZYZGOLNLR2KMZMF6HU37QPC2DXQKCABUHNKSRKGP";
            int index = 1;
            var add = await xlmClient.GenerateAddress(xPub, index);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "GC7Q6ZF35G6MIVLCZYZGOLNLR2KMZMF6HU37QPC2DXQKCABUHNKSRKGP";
            var address2 = "GDOE2VKNZHWTSLQ3RXZQLJWXPTUCITJICDOMNFH2AWM2YIERPXKJIQBM";
            var signatureId = "1b45e9db-b9ac-48ab-86aa-215ab9a3f14b";
            var r = await xlmClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = address1,
                    Amount = 128,
                    Fee = 1,
                    FromTag = 3,
                    ToTag = 2,
                    ToAddress = address2,
                    SignatureId = signatureId
                });
        }
    }
}
