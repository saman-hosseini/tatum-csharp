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
    public partial class BitcoinClient : BaseClient, IBitcoinClient
    {
        private readonly IBitcoinApi bitcoinApi;
        private readonly ITatumApi tatumApi;
        private static Precision Precision { get; } = Precision.Precision8;
        internal BitcoinClient()
        {
        }

        internal BitcoinClient(string apiBaseUrl, string xApiKey)
        {
            bitcoinApi = RestClientFactory.Create<IBitcoinApi>(apiBaseUrl, xApiKey);
            tatumApi = RestClientFactory.Create<ITatumApi>(apiBaseUrl, xApiKey);
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
            decimal remain = TatumHelper.ToDecimal(balance - amount, Precision);
            return (result, remain);
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

        async Task<Signature> SendTransactionKMSOld(TransferBlockchainKMS transfer)
        {
            var allUxtos = await GetAllUxto(transfer.FromAddress);
            var totalSatoshi = TatumHelper.ToLong(transfer.Amount + 0.0003M, Precision);
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

            var txHash = await bitcoinApi.SendTransactionKMS(sendObj);
            return txHash;
        }

        async Task<Signature> IBaseClient.SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var sendObj = new TransferBtcBasedBlockchainKMS()
            {
                FromAddresses = new List<FromAddressKMS>()
                    {
                        new FromAddressKMS()
                        {
                            Address = transfer.FromAddress,
                            SignatureId = transfer.SignatureId
                        }
                    },
                Tos = new List<To>()
                    {
                        new To()
                        {
                            Address = transfer.ToAddress,
                            Value = transfer.Amount
                        }
                    }
            };

            var txHash = await bitcoinApi.SendTransactionKMS(sendObj);
            return txHash;
        }

        public override async Task<Signature> SendLedgerKMS(TransferLedgerKMS transfer)
        {
            var fee = await bitcoinApi.EstimateFee(new BitcoinEstimateFee()
            {
                SenderAccountId = transfer.SenderAccountId,
                Address = transfer.ToAddress,
                Amount = transfer.Amount.ToString(),
                Xpub = transfer.XPub
            });
            var sendObj = new OffchainTransferBtcKMS()
            {
                Amount = transfer.Amount.ToString(),
                BlockchainAddress = transfer.ToAddress,
                Compliant = false,
                Fee = fee.Medium,
                SignatureId = transfer.SignatureId,
                SenderAccountId = transfer.SenderAccountId,
                Xpub = transfer.XPub,
                PaymentId = "1",
                SenderNote = "1"
            };
            var signature = await tatumApi.OffchainTransferBtc(new OffchainTransferBtcKMS()
            {
                SignatureId = transfer.SignatureId,
                Fee = transfer.Fee.ToString(),
                Amount = transfer.Amount.ToString(),
                Compliant = false,
                BlockchainAddress = transfer.ToAddress,
                Xpub = transfer.XPub,
                SenderAccountId = transfer.SenderAccountId,
                PaymentId = "1",
                SenderNote = "1"
            });
            return signature;
        }

        public async Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            var address = await bitcoinApi.GenerateAddress(xPubString, index);
            return new GenerateAddressResponse() { Address = address.Address, BlockchainAddressType = BlockchainAddressType.ReceiveAddress };
        }
    }

}
