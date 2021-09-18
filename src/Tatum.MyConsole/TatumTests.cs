using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Tatum.Clients;
using Tatum.Model.Requests;

namespace Tatum.MyConsole
{
    public class TatumTests
    {
        ITatumClient tatumClient;
        string accountId1 = "612d70faddedfc47cc08d1be";
        string accountId2 = "6133c4678f6d53c9087f64af";
        string accountId3 = "6134591d10c9ac177111f3df";
        string pk3 = "b651a0e471b370783c2e7c8ae1f1445da33b34e4aa0c818db8729fdf07c763f5";
        string coinexTronAddress = "TC5kLNd3A7fnxJfYSbUs99F4Qh4C1K47HV";
        public void Setup()
        {
            IConfiguration config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
           .Build();

            string baseUrl = config.GetValue<string>("TatumApiSettings:baseUrl");
            string xApiKey = config.GetValue<string>("TatumApiSettings:xApiKey");

            tatumClient = TatumClient.Create(baseUrl, xApiKey);
        }


        public void EndpointTest()
        {
            var credits = tatumClient.GetCreditUsageForLastMonth();
        }

        public async Task CreateAccount()
        {
            var account = new CreateAccount()
            {
                Currency = "TRON",
                //Xpub = "033dd961ca356b6c9b0af052781895d564b22b3650decb9f5bc218a75a9b5dc007b36f9250ff2eb91359d307032c358c6e3c22f2a793f2dbf8196f8ff1ead35af4",
                Compliant= false,
                AccountCode = "code_2",
                AccountingCurrency = "USD",
                AccountNumber = "2"
            };
            var newAccount = await tatumClient.CreateAccount(account);
        }

        public async Task GetAccounts()
        {
            var accounts = await tatumClient.GetAccounts(10, 0);
        }

        public async Task GetAccount()
        {
            var account = await tatumClient.GetAccount(accountId2);
        }

        public async Task AssignDepositAddress()
        {
            string address = "TNNNryU7ZbUmVKfCqCWnWhC9YXQhxdraC5";
            var account = await tatumClient.AssignDepositAddress(accountId2, address);
        }

        public async Task CheckAddressExists()
        {
            string address = "TNNNryU7ZbUmVKfCqCWnWhC9YXQhxdraC5";
            var currency = "USDT";
            var account = await tatumClient.CheckAddressExists(address, currency, "");
        }

        public async Task RemoveDepositAddress()
        {
            string address = "TNNNryU7ZbUmVKfCqCWnWhC9YXQhxdraC5";
            await tatumClient.RemoveDepositAddress(accountId2, address);
        }

        //transfer between private ledgers
        public async Task StoreTransaction()
        {
            var transaction = new CreateTransaction() 
            {
                SenderAccountId = accountId1,
                RecipientAccountId = accountId3,
                Amount = "51",
                SenderNote = "test2"
            };
            var reference = await tatumClient.StoreTransaction(transaction);
        }

        public async Task OffchainStoreWithdrawal()
        {
            var withdrawal = new CreateWithdrawal()
            {
                SenderAccountId = accountId1,
                Address = coinexTronAddress,
                Amount = "50.9",
                Fee = "0",
                SenderNote = "withdrawal first test"
            };
            var withdrawalResponse = await tatumClient.OffchainStoreWithdrawal(withdrawal);
        }

        public async Task OffchainGetWithdrawals()
        {
            var currency = "TRON";
            var status = "InProgress";//"InProgress" "Done" "Cancelled"
            var pageSize = 50;
            var offset = 0;
            var withdrawalResponse = await tatumClient.OffchainGetWithdrawals(currency, status);
        }

        public async Task OffchainCompleteWithdrawal()
        {
            string withdrawalId = "614077ebf95545813af50eed";
            string txId = "614077ebf95545813af50eed";
            await tatumClient.OffchainCompleteWithdrawal(withdrawalId, txId);
        }

        public async Task OffchainCancelWithdrawal()
        {
            string withdrawalId = "6140673628340bb12f39440b";
            await tatumClient.OffchainCancelWithdrawal(withdrawalId);
        }

        public async Task GetPendingTransactionsKms()
        {
            string blockchain = "TRON";
            var transactions = await tatumClient.GetPendingTransactionsKms(blockchain);
        }

        public async Task OffchainTransferTron()
        {
            string withdrawalId = "6134bbc03201f6fb0a2b7de4";
            string txId = "";
            var obj = new OffchainTransfer()
            {
                BlockchainAddress = coinexTronAddress,
                Amount = "51.5",
                Compliant = false,
                PrivateKey = pk3,
                Fee = "0.1",
                PaymentId = "1",
                SenderAccountId = accountId3,
                SenderNote = "first offchain"
            };
            var result = await tatumClient.OffchainTransferTron(obj);
        }
    }
}
