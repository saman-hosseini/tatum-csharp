using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tatum.Model.Requests;
using Tatum.Model.Responses;

namespace Tatum.Clients
{
    public interface ITronClient
    {
        Task<TransactionHash> Broadcast(BroadcastRequest request);
        Task<TronInfo> GetBlockchainInfo();
        Task<TronBlock> GetBlock(string hash);

        Task<TronAccount> GetTronAccount(string address);

        Wallet CreateWallet(string mnemonic, bool testnet);

        string GeneratePrivateKey(string mnemonic, int index, bool testnet);

        string GenerateAddress(string xPubString, int index, bool testnet);

        Task<TransactionHash> SendTransactionKMS(TransferTronBlockchainKMS body);
    }
}
