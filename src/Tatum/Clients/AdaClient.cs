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
        private static Precision Precision { get; } = Precision.Precision6;
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

        Task<Signature> IAdaClient.SendTransactionKMS(TransferBtcBasedBlockchainKMS transfer)
        {
            return adaApi.SendTransactionKMS(transfer);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var account = await adaApi.GetAccount(request.Address);
            var balance = account.Summary.AssetBalances.Where(x => x.Asset.AssetId.ToUpper() == request.Currency)
                .Sum(q => long.Parse(q.Quantity));
            return TatumHelper.ToDecimal(balance, Precision);
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

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
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
                    },
                
            };
            var txHash = await adaApi.SendTransactionKMS(sendObj);
            return txHash;
        }

        public async Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            var address = await adaApi.GenerateAddress(xPubString, index);
            return new GenerateAddressResponse() { Address = address.Address, BlockchainAddressType = BlockchainAddressType.ReceiveAddress };
        }
    }
}
