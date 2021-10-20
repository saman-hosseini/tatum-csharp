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
            var address = "no71xUudiRSBbYwe8QNXWZB8Zz25Nr65g7";
            var response = await dogecoinClient.GetBalance(
                new BalanceRequest() 
                {
                    Address = address
                });
        }

        public async Task GenerateAddress()
        {
            string xPub = "tpubDFeJdScxMsBg4raNVftTuyx54EeTNu4K7H9vYozyUTNBMU5BrygwbepWHsHU5wU79E6yuHfz5pCCqt1MZP3823QB2jWadFSaH3om86GiTmg";
            int index = 1;
            var add = await dogecoinClient.GenerateAddress(xPub, index);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "no71xUudiRSBbYwe8QNXWZB8Zz25Nr65g7";
            var address2 = "nZGmHHXPcorDbiREihKnuBfhssrGPeHmEs";
            var accountId = "615473eace3de8d452520e84";
            var signatureId = "ac0eb7ea-5fb1-4612-93c1-82900a09eafa";
            var xpub = "tpubDFeJdScxMsBg4raNVftTuyx54EeTNu4K7H9vYozyUTNBMU5BrygwbepWHsHU5wU79E6yuHfz5pCCqt1MZP3823QB2jWadFSaH3om86GiTmg";
            var req = new TransferBlockchainKMS()
            {
                FromAddress = address1,
                ToAddress = address2,
                Amount = 1M,
                SignatureId = signatureId,
                SenderAccountId = accountId,
                XPub = xpub
            };
            var response = await dogecoinClient.SendTransactionKMS(req);
        }
    }
}
