﻿using Microsoft.Extensions.Configuration;
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
    public class PolygonTests
    {
        IPolygonClient polygonClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            polygonClient = PolygonClient.Create(baseUrl, xApiKey);
        }

        public async Task GetBalance()
        {
            var Maddress = "0xE4bdCe3fEe7Cd2d722580b0e701531BAe004B85B";
            var req = new BalanceRequest()
            {
                Address = Maddress
            };
            var response = await polygonClient.GetBalance(req);
        }

        public async Task GenerateAddress()
        {
            string xPub = "xpub6FGg7CoGebEDo4PX3UuNi4chj7YoPWZFZeZdCFwq8YcHcBQUnkqrTD9g3qCgZSWsMKa5HiA7YiNA9hWkAXBt8bwsy8mGwinLh6DMN4iqoHU";
            int index = 1;
            var add = await polygonClient.GenerateAddress(xPub, index);
        }
    }
}
