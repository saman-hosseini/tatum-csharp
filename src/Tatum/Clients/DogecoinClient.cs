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
    public class DogecoinClient : IDogecoinClient
    {
        private readonly IDogecoinApi dogecoinApi;
        private readonly IDogechainApi dogechainApi;
        private const string dogechainUrl = "https://dogechain.info/";
        internal DogecoinClient()
        {
            
        }

        internal DogecoinClient(string apiBaseUrl, string xApiKey)
        {
            dogecoinApi = RestClientFactory.Create<IDogecoinApi>(apiBaseUrl, xApiKey);
            dogechainApi = RestClientFactory.Create<IDogechainApi>(dogechainUrl);
        }

        public static IDogecoinClient Create(string apiBaseUrl, string xApiKey)
        {
            return new DogecoinClient(apiBaseUrl, xApiKey);
        }

        Task<DogecoinBalance> IDogecoinClient.GetBalance(string address)
        {
            return dogechainApi.GetBalance(address);
        }

        Task<TransactionHash> IDogecoinClient.SendTransactionKMS(TransferDogecoinBlockchainKMS transfer)
        {
            return dogecoinApi.SendTransactionKMS(transfer);
        }

        private static decimal ToDecimalDoge(long amount)
        {
            return amount / 100000000M;
        }

        private static long ToLongDoge(decimal amount)
        {
            return decimal.ToInt64(amount * 100000000);
        }

        private static List<FromUtxoDogecoinKMS> ConvertToUtxoKMS(List<DogecoinUtxo> utxos, string signatureId)
        {
            return utxos.Select(q =>
                new FromUtxoDogecoinKMS()
                {
                    SignatureId = signatureId,
                    Index = q.TxOutputIndex,
                    TxHash = q.TxHash,
                    Value = q.Value,
                }).ToList();
        }

        private static (List<DogecoinUtxo> Utxos, decimal Remain) GetNeededUxto(List<DogecoinUtxo> allUxtos, long amount)
        {
            List<DogecoinUtxo> result = new();
            long balance = 0;
            foreach (var tx in allUxtos)
            {
                var value = ConvertToLong(tx.Value);
                if (value > 0)
                {
                    balance += value;
                    result.Add(tx);
                    if (balance > amount)
                    {
                        break;
                    }
                }
            }
            decimal remain = ToDecimalDoge(balance - amount);
            return (result, remain);
        }

        public async Task<TransactionHash> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var allUxtos = await dogechainApi.GetUnspentOutputs(transfer.FromAddress);
            var totalSatoshi = ToLongDoge(transfer.Amount + transfer.Fee);
            var uxtos = GetNeededUxto(allUxtos.UnspentOutputs, totalSatoshi);
            var sendObj = new TransferDogecoinBlockchainKMS()
            {
                ChangeAddress = transfer.FromAddress,
                Fee = transfer.Fee.ToString(),
                FromUTXO = ConvertToUtxoKMS(uxtos.Utxos, transfer.SignatureId),
                To = new List<ToDogecoin>()
                {
                    new ToDogecoin()
                    {
                        Address = transfer.ToAddress,
                        Value = Decimal.ToInt32(transfer.Amount)
                    }
                }
            };
            var tx = await dogecoinApi.SendTransactionKMS(sendObj);
            return tx;
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var dogecoinBalance = await dogechainApi.GetBalance(request.Address);
            var balance = decimal.Parse(dogecoinBalance.Balance);
            return balance;
        }

        private static long ConvertToLong(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            long.TryParse(str, out long result);
            return result;
        }
    }
}
