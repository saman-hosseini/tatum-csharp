using NBitcoin;
using System;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;

namespace TatumPlatform.Clients
{
    public partial class TronClient
    {
        Wallet ITronClient.CreateWallet(string mnemonic, bool testnet)
        {
            var tsk = Task.Run(async () => await tronApi.GenerateTronWallet(mnemonic));
            var wallet = tsk.Result;
            return wallet;
        }

        string ITronClient.GeneratePrivateKey(string mnemonic, int index, bool testnet)
        {
            var request = new TronPrivateKeyRequest()
            {
                Index = index,
                Mnemonic = mnemonic
            };
            var tsk = Task.Run(async () => await tronApi.GenerateTronPrivateKey(request));
            var privateKey = tsk.Result;
            return privateKey.Key;
        }
    }
}
