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
    public class OneTests
    {
        IOneClient oneClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            oneClient = OneClient.Create(baseUrl, xApiKey);
            oneClient.Currency = "ONE";
        }

        public async Task GetBalance()
        {
            var Maddress = "0xb5a0bf77a775eaecf45dea79798f90a9f628194d";
            var req = new BalanceRequest()
            {
                Address = Maddress
            };
            var response = await oneClient.GetBalance(req);
        }

        public async Task GenerateAddress()
        {
            string xPub = "xpub6EKUpbjwvtMDHKo6W3uGXxU784CiQXzJg8u7f2ygmZDkAJJqLkVJ3VhMWvF4s2Mu1owWvdabxsdShrpqkKMhEwaTzZ6ijrLezqDuiyAYh7o";
            int index = 1;
            var add = await oneClient.GenerateAddress(xPub, index);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "0xb5a0bf77a775eaecf45dea79798f90a9f628194d";
            var address2 = "0xe8316f4001acd808f47fbbf0034d72c8c6db30b4";
            var signatureId = "45128d92-a46f-4d2a-9543-8bdb5221a389";
            var r = await oneClient.SendTransactionKMS(
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
