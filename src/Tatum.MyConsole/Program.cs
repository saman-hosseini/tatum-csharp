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
            var ada = new AdaTests();
            ada.Setup();

            var doge = new DogecoinTests();
            doge.Setup();
            //var tsk = Task.Run(async () => await ada.GetAccount()); tsk.Wait();
            var tsk = Task.Run(async () => await doge.GetBalance()); tsk.Wait();
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
