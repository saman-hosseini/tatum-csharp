using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TatumPlatform.MyConsole.Model;

namespace TatumPlatform.MyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var db = new MyContext())
            //{
            //    var result = db.MyTest.OrderBy("JobId").Take(5).ToList();
            //}

            #region
            var btc = new BitcoinTests();
            btc.Setup();
            var tatum = new TatumTests();
            tatum.Setup();
            var tron = new TronTests();
            tron.Setup();

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
            //eth.Find();
            var tsk1 = Task.Run(async () => await btc.SendTransactionKMS()); tsk1.Wait();
            Console.ReadLine();

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
            #endregion
        }

    }
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderColumn, bool ascOrder = true)
        {
            var expression = source.Expression;
            var parameter = Expression.Parameter(typeof(T), "x");
            var selector = Expression.PropertyOrField(parameter, orderColumn);
            var method = ascOrder ? "OrderBy" : "OrderByDescending";
            expression = Expression.Call(typeof(Queryable), method,
                new Type[] { source.ElementType, selector.Type },
                expression, Expression.Quote(Expression.Lambda(selector, parameter)));
            return source.Provider.CreateQuery<T>(expression);
        }
    }
}
