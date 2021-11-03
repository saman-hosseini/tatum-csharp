using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Clients;
using TatumPlatform.Model.Requests;

namespace TatumPlatform.MyConsole
{
    public class BitcoinTests
    {
        IBitcoinClient bitcoinClient;
        readonly string mnemonic = "divert bread quantum tobacco key they one leaf good forward erupt split kitten maid mean crime youth chief jungle mind design broken tilt bus";

        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            bitcoinClient = BitcoinClient.Create(baseUrl, xApiKey);
        }

        public async Task EndpointTest()
        {
            var response = await bitcoinClient.GetBlockchainInfo();
        }
        public async Task GetBalance()
        {
            var Maddress = "mkbZK4vpCSvTxr94frf9vw88csLNxAhVB1";
            var req = new BalanceRequest()
            {
                Address = Maddress
            };
            var response = await bitcoinClient.GetBalance(req);
        }

        public async Task SendTransactionKMS()
        {
            var btc1 = "mkbZK4vpCSvTxr94frf9vw88csLNxAhVB1";
            var btc2 = "mzRozGK4J4WRxgfU5ZhtYJdZJyWBwiJuQm";
            var btc3 = "n4GRF6TitaPYHFpJqxhF6Bg38rD27kMDuH";
            var signatureId = "a14d9315-7f84-4c30-8f89-7c2775f95aad";
            var btcold1 = "34xp4vRoCGJym3xR7yCVPFHoCNxv4Twseo";
            var btcold2 = "16ftSEQ4ctQFDtVZiUBusQUjRrGhM3JYwe";
            //var r = await bitcoinClient.SendTransactionKMS(
            //    new TransferBlockchainKMS()
            //    {
            //        FromAddress = btc1,
            //        Amount = 0.01208971M,
            //        Fee = 0.00001M,
            //        ToAddress = btc2,
            //        SignatureId = signatureId
            //    });
            //var response1 = await bitcoinClient.SendLedgerKMS(new TransferLedgerKMS()
            //{
            //    Amount = "0.0003",
            //    SenderAccountId = "6165623e00a4f26048c49180",
            //    SignatureId = "c380bebc-9f1a-4740-a814-fa427e31c1f5",
            //    ToAddress = "mxoCTTj4yKY925JqRg7rb74NUHJNWhSwrw",
            //    XPub = "tpubDFeJdScxMsBg4raNVftTuyx54EeTNu4K7H9vYozyUTNBMU5BrygwbepWHsHU5wU79E6yuHfz5pCCqt1MZP3823QB2jWadFSaH3om86GiTmg"
            //});

            var response = await bitcoinClient.SendTransactionKMS(new TransferBtcBasedKMS()
            {

                Amount = 0.000024M,
                SenderAccountId = "6165623e00a4f26048c49180",
                SignatureId = "c380bebc-9f1a-4740-a814-fa427e31c1f5",
                MultipleAmounts = new string[] { "0.000011", "0.000013" },
                ToAddresses = new string[] { "mxoCTTj4yKY925JqRg7rb74NUHJNWhSwrw", "mwFw8ZNKG8W7FkQWy3UHmBeypH7MaR9UUL" },
                XPub = "tpubDFeJdScxMsBg4raNVftTuyx54EeTNu4K7H9vYozyUTNBMU5BrygwbepWHsHU5wU79E6yuHfz5pCCqt1MZP3823QB2jWadFSaH3om86GiTmg"
            });
        }

        public async Task GenerateAddress()
        {
            string xPub = "tpubDFeJdScxMsBg4raNVftTuyx54EeTNu4K7H9vYozyUTNBMU5BrygwbepWHsHU5wU79E6yuHfz5pCCqt1MZP3823QB2jWadFSaH3om86GiTmg";
            var index = 2;
            var add = await bitcoinClient.GenerateAddress(xPub, index);
        }

        public async Task GetTransactionFee()
        {
            var hash = "d65cda43ed2013fd11875c84bc9a2b2bfc1fd3a8bbf963f4743af029a512aeee";
            var fee = await bitcoinClient.GetTransactionFee(hash);
        }
    }
}
