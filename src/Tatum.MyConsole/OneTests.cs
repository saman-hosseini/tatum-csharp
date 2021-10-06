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
        }

        public async Task GetBalance()
        {
            var Maddress = "one1k67d4pme0lefp3zt77mcwpvay33el9wfqg2azm";
            var req = new BalanceRequest()
            {
                Address = Maddress
            };
            var response = await oneClient.GetBalance(req);
        }
    }
}
