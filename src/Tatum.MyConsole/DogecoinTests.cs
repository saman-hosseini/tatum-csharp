using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Clients;

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
            var response = await dogecoinClient.GetBalance(address);
        }
    }
}
