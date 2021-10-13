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
    public class XdcTests
    {
        IXdcClient xdcClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            xdcClient = XdcClient.Create(baseUrl, xApiKey);
            xdcClient.Currency = "XDC";
        }

        public async Task GetBalance()
        {
            var Maddress = "xdcb5a0bf77a775eaecf45dea79798f90a9f628194d";
            var req = new BalanceRequest()
            {
                Address = Maddress
            };
            var response = await xdcClient.GetBalance(req);
        }

        public async Task GenerateAddress()
        {
            string xPub = "xdcb5a0bf77a775eaecf45dea79798f90a9f628194d";
            int index = 1;
            var add = await xdcClient.GenerateAddress(xPub, index);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "xdcb5a0bf77a775eaecf45dea79798f90a9f628194d";
            var address2 = "xdce8316f4001acd808f47fbbf0034d72c8c6db30b4";
            var signatureId = "ad347649-7b0e-495d-b02b-8072f2be555b";
            var r = await xdcClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = address1,
                    Amount = 125,
                    Fee = 1,
                    Index = 1,
                    ToAddress = address2,
                    SignatureId = signatureId
                });
        }
    }
}
