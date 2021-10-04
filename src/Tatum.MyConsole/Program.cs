using System;
using System.Threading.Tasks;

namespace TatumPlatform.MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var tatum = new TatumTests();
            tatum.Setup();
            var tron = new TronTests();
            tron.Setup();
            var btc = new BitcoinTests();
            btc.Setup();

            var doge = new DogecoinTests();
            doge.Setup();

            var eth = new EthTests();
            eth.Setup();

            var bnb = new BnbTests();
            bnb.Setup();

            var bsc = new BscTests();
            bsc.Setup();

            var tsk1 = Task.Run(async () => await bsc.GetBalance()); tsk1.Wait();
            //var tsk2 = Task.Run(async () => await doge.SendTransactionKMS()); tsk2.Wait();
            //var tsk = Task.Run(async () => await btc.SendTransactionKMS()); tsk.Wait();
            //var tsk = Task.Run(async () => await doge.GetBalance()); tsk.Wait();
            //var tsk = Task.Run(async () => await tatum.GenerateDepositAddress()); tsk.Wait();
            //coin.CreateWalletTestNet();
            //coin.GeneratePrivateKeyTestNet();
            //coin.GenerateAddressTestNet();
            //coin.CreateWalletMainNet();
            //coin.GenerateAddressMainNet();
            //coin.GeneratePrivateKeyMainNet();
            //var tsk = Task.Run(async () => await coin.CreateAccount());
            //var tsk = Task.Run(async () => await tatum.OffchainStoreWithdrawal());tsk.Wait();
            //var tsk = Task.Run(async () => await tron.GetAccount());tsk.Wait();
            //var tsk = Task.Run(async () => await tatum.OffchainGetWithdrawals());tsk.Wait(); 
            //var tsk = Task.Run(async () => await tatum.OffchainCompleteWithdrawal());tsk.Wait();
            Console.WriteLine("Hello World!");
        }
    }
}
