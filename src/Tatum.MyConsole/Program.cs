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
            var ada = new AdaTests();
            ada.Setup();
            var doge = new DogecoinTests();
            doge.Setup();

            var eth = new EthTests();
            eth.Setup();

            var bnb = new BnbTests();
            bnb.Setup();

            var bsc = new BscTests();
            bsc.Setup();

            var celo = new CeloTests();
            celo.Setup();

            var xlm = new XlmTests();
            xlm.Setup();

            var xdc = new XdcTests();
            xdc.Setup();

            var egld = new EgldTests();
            egld.Setup();

            var flow = new FlowTests();
            flow.Setup();

            var one = new OneTests();
            one.Setup();

            var poly = new PolygonTests();
            poly.Setup();

            var vet = new VeChainTests();
            vet.Setup();

            var xrp = new XrpTests();
            xrp.Setup();

            var lite = new LitecoinTests();
            lite.Setup();

            var bcash = new BitcoinCashTests();
            bcash.Setup();

            var hmac = new HMACDigestTests();
            //hmac.Test();
            eth.Find();
            var tsk1 = Task.Run(async () => await tron.GetBalance()); tsk1.Wait();
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
