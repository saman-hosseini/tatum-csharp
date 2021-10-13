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
    public class BnbTests
    {
        IBinanceClient binanceClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            binanceClient = BinanceClient.Create(baseUrl, xApiKey);
            binanceClient.Currency = "BNB";
        }

        public async Task GetBalance()
        {
            binanceClient.Currency = "BNB";
            var address = "bnb1z35wusfv8twfele77vddclka9z84ugywug48gn";
            var req = new BalanceRequest()
            {
                Address = address
            };
            var response = await binanceClient.GetBalance(req);
        }

        public async Task SendTransactionKMS()
        {
            binanceClient.Currency = "ETH";
            var address1 = "0x307eaba8b2c0f756d64d7ee704b9e88954fca8a9";
            var address2 = "0xbd76e88a1abf05d1c49803dab874841570570ea9";
            var req = new TransferBlockchainKMS()
            {
                FromAddress = address1,
                ToAddress = address2,
                Amount = 0.0042M,
                Fee = 0.001M,
                SignatureId = "3be381a1-d149-4f86-9c58-b4626b0f502f"
            };
            var response = await binanceClient.SendTransactionKMS(req);
        }
    }
}
