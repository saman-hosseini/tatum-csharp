using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public interface ITronClient
    {
        Task<TransactionHash> Broadcast(BroadcastRequest request);
        Task<TronInfo> GetBlockchainInfo();
        Task<TronBlock> GetBlock(string hash);

        Task<TronAccount> GetAccount(string address);

        Wallet CreateWallet(string mnemonic, bool testnet);

        string GeneratePrivateKey(string mnemonic, int index, bool testnet);

        string GenerateAddress(string xPubString, int index, bool testnet);

        Task<TransactionHash> SendTransactionKMS(TransferTronBlockchainKMS transfer);
    }
}
