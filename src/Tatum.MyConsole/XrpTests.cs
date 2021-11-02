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
    public class XrpTests
    {
        IXrpClient xrpClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            xrpClient = XrpClient.Create(baseUrl, xApiKey);
        }

        public async Task GetBalance()
        {
            var Maddress = "rG4WqRWBU93h4P99qeb5EsnQDDTNNUymiq";
            var req = new BalanceRequest()
            {
                Address = Maddress
            };
            var response = await xrpClient.GetBalance(req);
        }

        public async Task GenerateAddress()
        {
            string xPub = "rG4WqRWBU93h4P99qeb5EsnQDDTNNUymiq";
            int index = 1;
            var add = await xrpClient.GenerateAddress(xPub, index);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "rERta5tv9twksVVCKZwXTFty21sFyLpTKV";
            var address2 = "rNopBjEd3Ck94VE7to3X26VQL98F5BtGCr";
            var signatureId = "debe7373-303e-40e5-aed6-b6cb78cb191f";
            var r = await xrpClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = address1,
                    Amount = 1,
                    FromTag = 34,
                    ToTag = 41,
                    ToAddress = address2,
                    SignatureId = signatureId
                });
        }

        public async Task GetTransactionFee()
        {
            var hash = "60721B5A5B6F13246183012600AEE0F5AA77FB59C3139B86321F5041BB3F7B6D";
            var fee = await xrpClient.GetTransactionFee(hash);
        }
    }
}
