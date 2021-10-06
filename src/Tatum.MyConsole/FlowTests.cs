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
    public class FlowTests
    {
        IFlowClient flowClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            flowClient = FlowClient.Create(baseUrl, xApiKey);
        }

        public async Task GetBalance()
        {
            var Taddress = "23452345";
            var Maddress = "0x18eb4ee6b3c026d2";
            var req = new BalanceRequest()
            {
                Address = Maddress
            };
            var response = await flowClient.GetBalance(req);
        }
    }
}
