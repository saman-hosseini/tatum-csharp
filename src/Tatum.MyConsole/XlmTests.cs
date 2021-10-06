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
        }

        public async Task GetBalance()
        {
            var Taddress = "23452345";
            var Maddress = "GDDSJUIDT5676C23QOIDP4YN6KK2X7QXCPJDCDPRUIWCP2CXXLRGKUPI";
            var req = new BalanceRequest()
            {
                Address = Taddress,
                Currency = "XLM"
            };
            var response = await xlmClient.GetBalance(req);
        }
    }
}
