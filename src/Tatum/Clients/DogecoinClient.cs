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
    public class DogecoinClient : BaseClient, IDogecoinClient
    {
        private readonly IDogecoinApi dogecoinApi;
        private readonly IDogechainApi dogechainApi;
        private const string dogechainUrl = "https://dogechain.info/";
        private static Precision Precision { get; } = Precision.Precision8;
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

        Task<Signature> IDogecoinClient.SendTransactionKMS(TransferDogecoinBlockchainKMS transfer)
        {
            return dogecoinApi.SendTransactionKMS(transfer);
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

        private static List<DogecoinUtxo> GetNeededUxto(List<DogecoinUtxo> allUxtos, long amount)
        {
            List<DogecoinUtxo> result = new();
            long balance = 0;
            foreach (var tx in allUxtos)
            {
                var value = TatumHelper.ToLong(tx.Value);
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
            return result;
        }

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var allUxtos = await dogechainApi.GetUnspentOutputs(transfer.FromAddress);
            var fee = await dogecoinApi.EstimateFee(new BitcoinEstimateFee()
            {
                SenderAccountId = transfer.SenderAccountId,
                Address = transfer.ToAddress,
                Amount = transfer.Amount.ToString(),
                Xpub = transfer.XPub
            });
            var totalSatoshi = TatumHelper.ToLong((transfer.Amount + TatumHelper.ToDecimal(fee.Medium)), Precision);
            var Utxos = GetNeededUxto(allUxtos.UnspentOutputs, totalSatoshi);
            var sendObj = new TransferDogecoinBlockchainKMS()
            {
                ChangeAddress = transfer.FromAddress,
                Fee = fee.Medium,
                FromUTXO = ConvertToUtxoKMS(Utxos, transfer.SignatureId),
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
            var balance = TatumHelper.ToDecimal(dogecoinBalance.Balance);
            return balance;
        }

        public async Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            var address = await dogecoinApi.GenerateAddress(xPubString, index);
            return new GenerateAddressResponse() { Address = address.Address, BlockchainAddressType = BlockchainAddressType.ReceiveAddress };
        }
    }
}
