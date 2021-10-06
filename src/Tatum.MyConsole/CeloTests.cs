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
        }

        public async Task GetBalance()
        {
            var address = "0x3173c4655487c0f7ad920701722e6a28c275b5c1";
            var req = new BalanceRequest()
            {
                Address = address
            };
            var response = await celoClient.GetBalance(req);
        }
    }
}
