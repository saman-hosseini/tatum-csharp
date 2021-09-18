using System;
using System.Threading.Tasks;

namespace Tatum.MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var tatum = new TatumTests();
            tatum.Setup();
            var tron = new TronTests();
            tron.Setup();
            //coin.CreateWalletTestNet();
            //coin.GeneratePrivateKeyTestNet();
            //coin.GenerateAddressTestNet();
            //coin.CreateWalletMainNet();
            //coin.GenerateAddressMainNet();
            //coin.GeneratePrivateKeyMainNet();
            //var tsk = Task.Run(async () => await coin.CreateAccount());
            //var tsk = Task.Run(async () => await tatum.OffchainStoreWithdrawal());tsk.Wait();
            var tsk = Task.Run(async () => await tron.SendTransactionKMS());tsk.Wait();
            //var tsk = Task.Run(async () => await tatum.OffchainGetWithdrawals());tsk.Wait(); 
            //var tsk = Task.Run(async () => await tatum.OffchainCompleteWithdrawal());tsk.Wait();
            Console.WriteLine("Hello World!");
        }
    }
}
