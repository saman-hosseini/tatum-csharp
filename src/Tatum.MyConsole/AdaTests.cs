﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tatum.Clients;

namespace Tatum.MyConsole
{
    public class AdaTests
    {
        IAdaClient adaClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            adaClient = AdaClient.Create(baseUrl, xApiKey);
        }

        public async Task GetAccount()
        {
            var address = "addr1qxkyasn5hs78mwc9ls9thfnrn5feslgrjpw4khz6pcze64m22klwgrcgwxtftp9l5wf03f4fhcrmn4yg8ycrjzy52t9s76es24";
            var response = await adaClient.GetAccount(address);
        }
    }
}
