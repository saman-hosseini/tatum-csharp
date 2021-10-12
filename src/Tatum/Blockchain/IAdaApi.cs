using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Blockchain
{
    public interface IAdaApi
    {
        [Get("/v3/ada/transaction/address/{address}?pageSize={pageSize}&offset={offset}")]
        Task<List<AdaTx>> GetTxForAccount(string address, int pageSize = 50, int offset = 0);

        [Get("/v3/ada/account/{address}")]
        Task<AdaAccount> GetAccount(string address);

        [Post("/v3/ada/transaction")]
        Task<Signature> SendTransactionKMS(TransferBtcBasedBlockchainKMS transfer);

        [Get("/v3/ada/{address}/utxos")]
        Task<List<AdaUtxo>> GetUtxos(string address);

        [Get("/v3/ada/address/{xpub}/{index}")]
        Task<BlockchainAddress> GenerateAddress(string xpub, int index);
    }
}
