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
    public class EthTests
    {
        IEthereumClient ethereumClient;
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            ethereumClient = EthereumClient.Create(baseUrl, xApiKey);
        }

        public async Task GetBalance()
        {
            ethereumClient.Currency = "ETH";
            var address = "0x307eaba8b2c0f756d64d7ee704b9e88954fca8a9";
            var req = new BalanceRequest()
            {
                Address = address
            };
            ethereumClient.Currency = "MT5";
            ethereumClient.ContractAddress = "0xdb91dbe3bb5f6c56f26f52f3b52361dd0744225a";
            ethereumClient.DecimalPrecision = 6;
            var response = await ethereumClient.GetBalance(req);
        }

        public async Task SendTransactionKMS()
        {
            ethereumClient.Currency = "ETH";
            var address1 = "0x307eaba8b2c0f756d64d7ee704b9e88954fca8a9";
            var address2 = "0x14C087a6c52CEEd605705f5C92d0f7090B560AF9";
            var SignaturePrivatekey = "3be381a1-d149-4f86-9c58-b4626b0f502f";
            var SignatureMnemonic = "977b8792-5aae-40de-a7dd-f0d41bc15fb8";
            var req = new TransferBlockchainKMS()
            {
                FromAddress = address1,
                ToAddress = address2,
                Amount = 3,
                Fee = 0.001M,
                Index = 1,
                SignatureId = SignatureMnemonic
            };
            var response = await ethereumClient.SendTransactionKMS(req);
        }

        public async Task GenerateAddress()
        {
            string xPub = "xpub6Dmec6dnYaMC3Vf34shR6AG1KnJS6j6oYyrEoALgt3SqNzf5eLkJUpb31UDG8oqxMQvXXiAP27gMJKkTvTyNuVrEbXrVwEHZFTaTxbD2hLy";
            int index = 1;
            var add = await ethereumClient.GenerateAddress(xPub, index);
        }

        public void Find()
        {
            string xPub = "xpub6DjUAbkzzUGRRyirEeEifXRtTzFXQQbd7m8yLBRsWm5qBHhJ8dQxDV99m1NhsehW16LXyRtsU5GLvEawtjFDbxRNk9RvR33SXHVFXasaXiF";
            string address = "0x14C087a6c52CEEd605705f5C92d0f7090B560Af9".ToLower();
            //toLower 14462000
            for (int i = 14462000; i < int.MaxValue; i++)
            {
                if (i % 1000 == 0)
                    Console.WriteLine(i);
                var add = ethereumClient.GenerateAddress(xPub, i, true).ToLower();
                if (address == add)
                {

                }
            }

        }
    }
}
