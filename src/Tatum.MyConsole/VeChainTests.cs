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
    public class VeChainTests
    {
        IVeChainClient veChainClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            veChainClient = VeChainClient.Create(baseUrl, xApiKey);
        }

        public async Task GetBalance()
        {
            var Maddress = "0x9eB35e1F29E643c2F673E4c61B40084c534099EC";
            var req = new BalanceRequest()
            {
                Address = Maddress
            };
            var response = await veChainClient.GetBalance(req);
        }

        public async Task SendTransactionKMS()
        {
            var btc1 = "mkbZK4vpCSvTxr94frf9vw88csLNxAhVB1";
            var btc2 = "mzRozGK4J4WRxgfU5ZhtYJdZJyWBwiJuQm";
            var r = await veChainClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = btc2,
                    Amount = 0.0003M,
                    Fee = 0.00003M,
                    FromTag = 3,
                    ToAddress = btc1,
                    SignatureId = "cPSSYukdVsEyqQtHZ9Ri9xjKGtM1RbGKiuX38XcHVamHwoVAkHjP"
                });
        }
    }
}
