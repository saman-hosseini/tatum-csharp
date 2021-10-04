using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class BitcoinClient : IBitcoinClient
    {
        private readonly IBitcoinApi bitcoinApi;

        internal BitcoinClient()
        {
        }

        internal BitcoinClient(string apiBaseUrl, string xApiKey)
        {
            bitcoinApi = RestClientFactory.Create<IBitcoinApi>(apiBaseUrl, xApiKey);
        }

        public static IBitcoinClient Create(string apiBaseUrl, string xApiKey)
        {
            return new BitcoinClient(apiBaseUrl, xApiKey);
        }

        Task<TransactionHash> IBitcoinClient.Broadcast(BroadcastRequest request)
        {
            var validationContext = new ValidationContext(request);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);

            return bitcoinApi.BroadcastSignedTransaction(request);
        }

        Task<BitcoinInfo> IBitcoinClient.GetBlockchainInfo()
        {
            return bitcoinApi.GetBlockchainInfo();
        }

        Task<BitcoinBlock> IBitcoinClient.GetBlock(string hash)
        {
            return bitcoinApi.GetBlock(hash);
        }

        Task<BlockHash> IBitcoinClient.GetBlockHash(int blockHeight)
        {
            return bitcoinApi.GetBlockHash(blockHeight);
        }

        Task<BitcoinUtxo> IBitcoinClient.GetUtxo(string txHash, int txOutputIndex)
        {
            return bitcoinApi.GetUtxo(txHash, txOutputIndex);
        }

        Task<List<BitcoinTx>> IBitcoinClient.GetTxForAccount(string address, int pageSize, int offset)
        {
            return bitcoinApi.GetTxForAccount(address, pageSize, offset);
        }

        Task<BitcoinTx> IBitcoinClient.GetTransaction(string hash)
        {
            return bitcoinApi.GetTransaction(hash);
        }

        Task<BitcoinAccountBalance> IBitcoinClient.GetAccountBalance(string address)
        {
            return bitcoinApi.GetAccountBalance(address);
        }

        async Task<decimal> IBaseClient.GetBalance(BalanceRequest request)
        {
            var balanceReq = await bitcoinApi.GetAccountBalance(request.Address);
            return balanceReq.CurrentBalance;
        }

        async Task<List<BitcoinUtxo>> GetAllUxto(string address)
        {
            List<BitcoinTx> allTransactions = new();
            List<BitcoinUtxo> allPrev = new();
            int i = 0;
            while (true)
            {
                var currentTransactions = await bitcoinApi.GetTxForAccount(address, 50, i * 50);
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
            decimal remain = SatoshiToBtc(balance - amount);
            return (result, remain);
        }

        private static decimal SatoshiToBtc(long amount)
        {
            return amount / 100000000M;
        }

        private static long BtcToSatoshi(decimal amount)
        {
            return decimal.ToInt64(amount * 100000000);
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

        private static List<FromUtxo> ConvertToUtxo(List<BitcoinUtxo> utxos, string privatekey)
        {
            return utxos.Select(q =>
                new FromUtxo()
                {
                    PrivateKey = privatekey,
                    Index = q.Index,
                    TxHash = q.Hash
                }).ToList();
        }
        async Task<TransactionHash> IBaseClient.SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var allUxtos = await GetAllUxto(transfer.FromAddress);
            var totalSatoshi = BtcToSatoshi(transfer.Amount + transfer.Fee);
            var uxtos = GetNeededUxto(allUxtos, totalSatoshi);

            foreach (var u in uxtos.Utxos)
            {
                var res = await bitcoinApi.GetUtxo(u.Hash, u.Index);
            }

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
            var txHash = await bitcoinApi.SendTransactionKMS(sendObj);
            return txHash;
        }
    }

}
