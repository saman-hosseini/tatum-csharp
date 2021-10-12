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

        public void CreateWalletMainNet()
        { 
            Wallet wallet = bitcoinClient.CreateWallet(mnemonic, false);
        }

        public async Task SendTransactionKMS()
        {
            var btc1 = "mkbZK4vpCSvTxr94frf9vw88csLNxAhVB1";
            var btc2 = "mzRozGK4J4WRxgfU5ZhtYJdZJyWBwiJuQm";
            var btc3 = "n4GRF6TitaPYHFpJqxhF6Bg38rD27kMDuH";
            var signatureId = "a14d9315-7f84-4c30-8f89-7c2775f95aad";
            var btcold1 = "34xp4vRoCGJym3xR7yCVPFHoCNxv4Twseo";
            var btcold2 = "16ftSEQ4ctQFDtVZiUBusQUjRrGhM3JYwe";
            var r = await bitcoinClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = btc3,
                    Amount = 0.00002M,
                    Fee = 0.00001M,
                    ToAddress = btc2,
                    SignatureId = signatureId
                });
        }

        public async Task GenerateAddressMainNet()
        {
            string xPub = "tpubDFeJdScxMsBg4raNVftTuyx54EeTNu4K7H9vYozyUTNBMU5BrygwbepWHsHU5wU79E6yuHfz5pCCqt1MZP3823QB2jWadFSaH3om86GiTmg";
            int index = 1;
            string address = bitcoinClient.GenerateAddress("xpub6EsCk1uU6cJzqvP9CdsTiJwT2rF748YkPnhv5Qo8q44DG7nn2vbyt48YRsNSUYS44jFCW9gwvD9kLQu9AuqXpTpM1c5hgg9PsuBLdeNncid", 1, false);
            var add = await bitcoinClient.GenerateAddress(xPub, index);
        }
    }
}
