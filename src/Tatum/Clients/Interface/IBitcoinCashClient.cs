﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public interface IBitcoinCashClient : IBaseClient
    {
        Task<TransactionHash> BroadcastSignedTransaction(BroadcastRequest request);
        Task<BitcoinCashInfo> GetBlockchainInfo();
        Task<BitcoinCashBlock> GetBlock(string hash);
        Task<BlockHash> GetBlockHash(long blockHeight);
        Task<List<BitcoinCashTx>> GetTxForAccount(string address, int skip = 0);
        Task<BitcoinCashTx> GetTransaction(string hash);


        /// <summary>
        /// Generate BitcoinCash wallet
        /// </summary>
        /// <param name="mnemonic"></param>
        /// <param name="testnet">testnet or mainnet version of address</param>
        /// <returns></returns>
        Wallet CreateWallet(string mnemonic, bool testnet);

        /// <summary>
        /// Generate BitcoinCash private key from mnemonic seed
        /// </summary>
        /// <param name="mnemonic">mnemonic to generate private key from</param>
        /// <param name="index">derivation index of private key to generate</param>
        /// <param name="testnet">testnet or mainnet version of address</param>
        /// <returns>blockchain private key</returns>
        string GeneratePrivateKey(string mnemonic, int index, bool testnet);

        /// <summary>
        /// Generate BitcoinCash address
        /// </summary>
        /// <param name="xPubString">extended public key to generate address from</param>
        /// <param name="index">derivation index of address to generate. Up to 2^31 addresses can be generated</param>
        /// <param name="testnet">testnet or mainnet version of address</param>
        /// <returns>blockchain address</returns>
        string GenerateAddress(string xPubString, int index, bool testnet);

        Task<Signature> SendTransactionKMS(TransferBchBlockchainKMS transferBtc);
        Task<string> SignKmsTransaction(TransactionKms tx, List<string> privateKeys, bool testnet);

        /// <summary>
        /// Sign BitcoinCash transaction with private keys locally. Nothing is broadcasted to the blockchain.
        /// </summary>        
        /// <param name="body">content of the transaction to broadcast</param>
        /// <param name="testnet">testnet or mainnet version</param>
        /// <returns>Transaction data to be broadcast to blockchain.</returns>
        string PrepareSignedTransaction(TransferBchBlockchain body, bool testnet);

        /// <summary>
        /// Send BitcoinCash transaction to the blockchain. This method broadcasts signed transaction to the blockchain.
        /// This operation is irreversible.
        /// </summary>
        /// <param name="body">content of the transaction to broadcast</param>
        /// <param name="testnet">testnet or mainnet version</param>
        /// <returns>transaction id of the transaction in the blockchain</returns>
        Task<TransactionHash> SendTransaction(TransferBchBlockchain body, bool testnet);
    }
}
