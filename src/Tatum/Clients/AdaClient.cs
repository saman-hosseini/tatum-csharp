using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class AdaClient : IAdaClient
    {
        private readonly IAdaApi adaApi;

        internal AdaClient()
        {
        }

        internal AdaClient(string apiBaseUrl, string xApiKey)
        {
            adaApi = RestClientFactory.Create<IAdaApi>(apiBaseUrl, xApiKey);
        }

        public static IAdaClient Create(string apiBaseUrl, string xApiKey)
        {
            return new AdaClient(apiBaseUrl, xApiKey);
        }

        Task<AdaAccount> IAdaClient.GetAccount(string address)
        {
            return adaApi.GetAccount(address);
        }

        Task<TransactionHash> IAdaClient.SendTransactionKMS(TransferBtcBasedBlockchainKMS transfer)
        {
            return adaApi.SendTransactionKMS(transfer);
        }

        private static decimal ToDecimalAda(long amount)
        {
            return amount / 1000000M;
        }

        private static long ToLongAda(decimal amount)
        {
            return decimal.ToInt64(amount * 1000000);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var account = await adaApi.GetAccount(request.Address);
            var balance = account.Summary.AssetBalances.Where(x => x.Asset.AssetId.ToUpper() == request.Currency)
                .Sum(q => long.Parse(q.Quantity));
            return ToDecimalAda(balance);
        }

        async Task<List<BitcoinUtxo>> GetAllUxto(string address)
        {
            List<BitcoinTx> allTransactions = new();
            List<BitcoinUtxo> allPrev = new();
            int i = 0;
            while (true)
            {
                var currentTransactions = await adaApi.GetTxForAccount(address, 50, i * 50);
                allPrev = allPrev.Union(currentTransactions.SelectMany(q =>
                    q.Inputs.Select(x => new BitcoinUtxo()
                    { Hash = x.Prevout.Hash, Address = x.Coin.Address }))).ToList();
                allTransactions = allTransactions.Union(currentTransactions).ToList();
                if (currentTransactions.Count == 0)
                {
                    break;
                }
                if (allPrev.Any(y => y.Hash == currentTransactions.Last().Hash && y.Address == address))
                {
                    break;
                }
                await Task.Delay(100);
                i++;
            }

            var uxtos = allTransactions.Where(x => !allPrev.Any(y => y.Hash == x.Hash && y.Address == address)).ToList();
            List<BitcoinUtxo> result = new();
            int oi = 0;
            uxtos.Sort((a, b) => a.Time.CompareTo(b.Time));
            foreach (var uxto in uxtos)
            {
                oi = 0;
                foreach (var item in uxto.Outputs)
                {
                    if (item.Address == address)
                    {
                        result.Add(new BitcoinUtxo()
                        {
                            Index = oi,
                            Value = item.Value,
                            Hash = uxto.Hash
                        });
                    }
                    oi++;
                }
            }
            return result;
        }

        private static List<FromUtxoKMS> ConvertToUtxoKMS(List<BitcoinUtxo> utxos, string signatureId)
        {
            return utxos.Select(q =>
                new FromUtxoKMS()
                {
                    SignatureId = signatureId,
                    Index = q.Index,
                    TxHash = q.Hash
                }).ToList();
        }

        private static (List<BitcoinUtxo> Utxos, decimal Remain) GetNeededUxto(List<BitcoinUtxo> allUxtos, long amount)
        {
            List<BitcoinUtxo> result = new();
            long balance = 0;
            foreach (var tx in allUxtos)
            {
                balance += tx.Value;
                result.Add(tx);
                if (balance > amount)
                {
                    break;
                }
            }
            decimal remain = ToDecimalAda(balance - amount);
            return (result, remain);
        }


        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var allUxtos = await GetAllUxto(transfer.FromAddress);
            var totalSatoshi = ToLongAda(transfer.Amount + transfer.Fee);
            var uxtos = GetNeededUxto(allUxtos, totalSatoshi);

            var sendObj = new TransferBtcBasedBlockchainKMS()
            {
                FromUtxos = ConvertToUtxoKMS(uxtos.Utxos, transfer.SignatureId),
                Tos = new List<To>()
                    {
                        new To()
                        {
                            Address = transfer.ToAddress,
                            Value = transfer.Amount
                        },
                        new To()
                        {
                            Address = transfer.FromAddress,
                            Value = uxtos.Remain
                        }
                    }
            };
            var txHash = await adaApi.SendTransactionKMS(sendObj);
            return txHash;
        }
    }
}
