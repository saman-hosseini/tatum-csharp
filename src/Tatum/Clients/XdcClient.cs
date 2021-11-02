using System.Threading.Tasks;
using TatumPlatform.Blockchain;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public class XdcClient : BaseClient, IXdcClient
    {
        private readonly IXdcApi xdcApi;

        internal XdcClient()
        {
        }

        internal XdcClient(string apiBaseUrl, string xApiKey)
        {
            xdcApi = RestClientFactory.Create<IXdcApi>(apiBaseUrl, xApiKey);
        }

        public static IXdcClient Create(string apiBaseUrl, string xApiKey)
        {
            return new XdcClient(apiBaseUrl, xApiKey);
        }

        Task<XdcBalance> IXdcClient.GetBalance(string address)
        {
            return xdcApi.GetBalance(address);
        }

        Task<Signature> IXdcClient.SendTransactionKMS(TransferXdcBlockchainKMS transfer)
        {
            return xdcApi.SendTransactionKMS(transfer);
        }

        public async Task<decimal> GetBalance(BalanceRequest request)
        {
            var balance = await xdcApi.GetBalance(request.Address);
            return TatumHelper.ToDecimal(balance.Balance);
        }

        public async Task<Signature> SendTransactionKMS(TransferBlockchainKMS transfer)
        {
            var fee = await xdcApi.EstimateFee(new EthereumEstimateFee()
            {
                From = transfer.FromAddress,
                To = transfer.ToAddress,
                Amount = transfer.Amount.ToString(),
                Data = transfer.Message
            });
            fee.GasPrice = TatumHelper.ToFormat(fee.GasPrice, 3);
            var req = new TransferXdcBlockchainKMS()
            {
                SignatureId = transfer.SignatureId,
                Amount = transfer.Amount.ToString(),
                Currency = Currency,
                Data = transfer.Message,
                Index = transfer.Index,
                To = transfer.ToAddress,
                Fee = fee
            };
            var tx = await xdcApi.SendTransactionKMS(req);
            return tx;
        }

        public override async Task<decimal> GetTransactionFee(string transactionHash)
        {
            var tx = await xdcApi.GetTransaction(transactionHash);
            return tx.Fee;
        }

        public async Task<GenerateAddressResponse> GenerateAddress(string xPubString, int index)
        {
            var address = await xdcApi.GenerateAddress(xPubString, index);
            return new GenerateAddressResponse() { Address = address.Address, BlockchainAddressType = BlockchainAddressType.ReceiveAddress };
        }
    }
}
