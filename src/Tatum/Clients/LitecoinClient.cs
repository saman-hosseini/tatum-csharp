using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    /// <inheritdoc/>
    public partial class LitecoinClient : ILitecoinClient
    {
        private readonly ILitecoinApi litecoinApi;
        private static Precision Precision { get; } = Precision.Precision8;
        internal LitecoinClient()
        {
        }

        internal LitecoinClient(string apiBaseUrl, string xApiKey)
        {
            litecoinApi = RestClientFactory.Create<ILitecoinApi>(apiBaseUrl, xApiKey);
        }

        public static ILitecoinClient Create(string apiBaseUrl, string xApiKey)
        {
            return new LitecoinClient(apiBaseUrl, xApiKey);
        }

        Task<TransactionHash> ILitecoinClient.BroadcastSignedTransaction(BroadcastRequest request)
        {
            var validationContext = new ValidationContext(request);
            Validator.ValidateObject(request, validationContext, validateAllProperties: true);

            return litecoinApi.BroadcastSignedTransaction(request);
        }

        Task<LitecoinBlock> ILitecoinClient.GetBlock(string hash)
        {
            return litecoinApi.GetBlock(hash);
        }

        Task<LitecoinInfo> ILitecoinClient.GetBlockchainInfo()
        {
            return litecoinApi.GetBlockchainInfo();
        }

        Task<BlockHash> ILitecoinClient.GetBlockHash(long blockHeight)
        {
            return litecoinApi.GetBlockHash(blockHeight);
        }

        Task<LitecoinTx> ILitecoinClient.GetTransaction(string hash)
        {
            return litecoinApi.GetTransaction(hash);
        }

        Task<List<LitecoinTx>> ILitecoinClient.GetTxForAccount(string address, int pageSize, int offset)
        {
            return litecoinApi.GetTxForAccount(address, pageSize, offset);
        }

        Task<LitecoinUtxo> ILitecoinClient.GetUtxo(string txHash, int txOutputIndex)
        {
            return litecoinApi.GetUtxo(txHash, txOutputIndex);
        }

        Task<Signature> ILitecoinClient.SendTransactionKMS(TransferBtcBasedBlockchainKMS transferBtc)
        {
            return litecoinApi.SendTransactionKMS(transferBtc);
        }

        Task<BitcoinAccountBalance> ILitecoinClient.GetAccountBalance(string address)
        {
            return litecoinApi.GetAccountBalance(address);
        }

        async Task<List<LitecoinUtxo>> GetAllUxto(string address)
        {
            List<LitecoinTx> allTransactions = new();
            List<LitecoinUtxo> allPrev = new();
            int i = 0;
            while (true)
            {
                var currentTransactions = await litecoinApi.GetTxForAccount(address, 50, i * 50);
                allPrev = allPrev.Union(currentTransactions.SelectMany(q =>
                    q.Inputs.Select(x => new LitecoinUtxo()
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
            List<LitecoinUtxo> result = new();
            int oi = 0;
            uxtos.Sort((a, b) => a.Time.CompareTo(b.Time));
            foreach (var uxto in uxtos)
            {
                oi = 0;
                foreach (var item in uxto.Outputs)
                {
                    if (item.Address == address)
                    {
                        result.Add(new LitecoinUtxo()
                        {
                            Index = oi,
                            Value = long.Parse(item.Value),
                            Hash = uxto.Hash
                        });
                    }
                    oi++;
                }
            }
            return result;
        }

        private static (List<LitecoinUtxo> Utxos, decimal Remain) GetNeededUxto(List<LitecoinUtxo> allUxtos, long amount)
        {
            List<LitecoinUtxo> result = new();
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
            decimal remain = TatumHelper.ToDecimal(balance - amount, Precision);
            return (result, remain);
        }

        private static List<FromUtxoKMS> ConvertToUtxoKMS(List<LitecoinUtxo> utxos, string signatureId)
        {
            return utxos.Select(q =>
                new FromUtxoKMS()
                {
                    SignatureId = signatureId,
                    Index = q.Index,
                    TxHash = q.Hash
                }).ToList();
        }

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var allUxtos = await GetAllUxto(transfer.FromAddress);
            var totalSatoshi = TatumHelper.ToLong(transfer.Amount + transfer.Fee, Precision);
            var (Utxos, Remain) = GetNeededUxto(allUxtos, totalSatoshi);

            var sendObj = new TransferBtcBasedBlockchainKMS()
            {
                FromUtxos = ConvertToUtxoKMS(Utxos, transfer.SignatureId),
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
                            Value = Remain
                        }
                    }
            };
            var txHash = await litecoinApi.SendTransactionKMS(sendObj);
            return txHash;
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var balanceReq = await litecoinApi.GetAccountBalance(request.Address);
            return balanceReq.CurrentBalance;
        }

        public async Task<string> GenerateAddress(string xPubString, int index)
        {
            var address = await litecoinApi.GenerateAddress(xPubString, index);
            return address.Address;
        }
    }
}
