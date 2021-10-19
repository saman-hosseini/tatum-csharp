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
            var address1 = "addr1qxkyasn5hs78mwc9ls9thfnrn5feslgrjpw4khz6pcze64m22klwgrcgwxtftp9l5wf03f4fhcrmn4yg8ycrjzy52t9s76es24";
            var address2 = "addr1qxjssrst332m8t3v8q5wyy9s4mtu3esul25zq0qdngztx5d9pq8qhrz4kwhzcwpguggtptkhernpe74gyq7qmxsykdgsf8ze42";
            var response = await adaClient.GetAccount(address2);
        }

        public async Task GetBalance()
        {
            var address = "addr1qxjssrst332m8t3v8q5wyy9s4mtu3esul25zq0qdngztx5d9pq8qhrz4kwhzcwpguggtptkhernpe74gyq7qmxsykdgsf8ze42";
            var req = new BalanceRequest()
            {
                Address = address
            };
            var response = await adaClient.GetBalance(req);
        }

        public async Task GenerateAddress()
        {
            var xpub = "83c99dad32eed06f35be69616ac53c8ff958ab8e88fa7348fd552938abc3f785ba5f2301a30d65dcb55f70ca06a94f4950b72d48e2ead728096ad1cc970873f54789bb53a15bba6f71da29e482b7f1361f5f6f46f01e7dae587e6ae8541e2833efa832a5dfd1e6eebb78ca2a85e4822dd07a2bcaf51a2da79f75052d009358d6";
            int index = 1;
            var response = await adaClient.GenerateAddress(xpub, index);
        }

        public async Task SendTransactionKMS()
        {
            var address1 = "addr_test1qpptgq39yxhjk94w7cvdj25jy26lga8j5r8lg8stqt9c7xmdxh6zxjfanusvvudzg293el2vucx5hgwh8q34lwa0jqcq0d7de8";
            var address2 = "addr_test1qpnvxuyvtfwk3q37fuk0mzluhtlvwtu0qrxlavsrx79wccmdxh6zxjfanusvvudzg293el2vucx5hgwh8q34lwa0jqcqt7egrr";
            var SignatureMemonic = "42bc7569-83ea-4694-bb94-c312222b916e";
            var r = await adaClient.SendTransactionKMS(
                new TransferBlockchainKMS()
                {
                    FromAddress = address1,
                    Amount = 19,
                    ToAddress = address2,
                    SignatureId = SignatureMemonic
                });
        }

        public async Task GetTx()
        {
            var address1 = "addr1q8q9hycqwfmzup3829j3kjcnr3da2tlldcy27paz5k40jhkxl27u6zld06dgvmfnesmya75ef3g02js5kyk9zs0rf8mqfad6xj";
            var req = new BalanceRequest()
            {
                Address = address1
            };
            var response = await adaClient.GetBalance(req);
        }
    }
}
