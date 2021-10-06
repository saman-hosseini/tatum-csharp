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
    public class DogecoinTests
    {
        IDogecoinClient dogecoinClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            dogecoinClient = DogecoinClient.Create(baseUrl, xApiKey);
        }

        public async Task GetBalance()
        {
            var address = "DMr3fEiVrPWFpoCWS958zNtqgnFb7QWn9D";
            var response = await dogecoinClient.GetBalance(
                new BalanceRequest() 
                {
                    Address = address
                });
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "nZGmHHXPcorDbiREihKnuBfhssrGPeHmEs";
            var address2 = "no71xUudiRSBbYwe8QNXWZB8Zz25Nr65g7";
            var req = new TransferBlockchainKMS()
            {
                FromAddress = address1,
                ToAddress = address2,
                Amount = 0.0042M,
                Currency = "",
                Fee = 0.001M,
                SignatureId = "3be381a1-d149-4f86-9c58-b4626b0f502f"
            };
            var response = await dogecoinClient.SendTransactionKMS(req);
        }
    }
}
