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
    public interface IDogecoinApi
    {
        [Post("/v3/dogecoin/transaction")]
        Task<Signature> SendTransactionKMS(TransferDogecoinBlockchainKMS transfer);

        [Get("/v3/dogecoin/utxo/{hash}/{index}")]
        Task<DogecoinTransactionUtxo> GetTransactionUtxo(string hash, int index);

        [Get("/v3/dogecoin/address/{xpub}/{index}")]
        Task<BlockchainAddress> GenerateAddress(string xpub, int index);
    }
}
