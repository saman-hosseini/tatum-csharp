﻿using NBitcoin;
using NBitcoin.Altcoins;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TatumPlatform.Model.Requests;
using TatumPlatform.Model.Responses;

namespace TatumPlatform.Clients
{
    public partial class BitcoinCashClient : IBitcoinCashClient
    {
        Task<string> IBitcoinCashClient.SignKmsTransaction(TransactionKms tx, List<string> privateKeys, bool testnet)
        {
            throw new NotImplementedException();
        }

        Task<TransactionHash> IBitcoinCashClient.SendTransactionKMS(TransferBchBlockchainKMS transferBtc)
        {
            return bitcoinCashApi.SendTransactionKMS(transferBtc);
        }

        async Task<TransactionHash> IBitcoinCashClient.SendTransaction(TransferBchBlockchain body, bool testnet)
        {
            string txData = (this as IBitcoinCashClient).PrepareSignedTransaction(body, testnet);
            var broadcastRequest = new BroadcastRequest
            {
                TxData = txData
            };

            return await (this as IBitcoinCashClient).BroadcastSignedTransaction(broadcastRequest).ConfigureAwait(false);
        }

        string IBitcoinCashClient.PrepareSignedTransaction(TransferBchBlockchain body, bool testnet)
        {
            return PrepareSignedTransaction(testnet ? BCash.Instance.Testnet : BCash.Instance.Mainnet, body);
        }

        private string PrepareSignedTransaction(Network network, TransferBchBlockchain body)
        {
            var validationContext = new ValidationContext(body);
            Validator.ValidateObject(body, validationContext);

            NBitcoin.Transaction transaction = network.CreateTransaction();
            List<BitcoinSecret> privateKeysToSign = new List<BitcoinSecret>();
            List<Coin> coinsToSpent = new List<Coin>();

            if (body.FromUtxos != null)
            {
                foreach (FromUtxoBcash fromUtxo in body.FromUtxos)
                {
                    var secret = new BitcoinSecret(fromUtxo.PrivateKey, network);

                    try
                    {
                        TxIn input = GetInputFromUtxo(secret, fromUtxo, privateKeysToSign, coinsToSpent);
                        transaction.Inputs.Add(input);
                    }
                    catch (Exception)
                    {
                        // spent, unconfirmed or invalid utxos
                    }
                }
            }

            foreach (To to in body.Tos)
            {
                var outputAddress = BitcoinAddress.Create(to.Address, network);
                transaction.Outputs.Add(new Money(to.Value, MoneyUnit.BTC), outputAddress.ScriptPubKey);
            }

            transaction.Sign(privateKeysToSign, coinsToSpent);

            return transaction.ToHex();
        }

        private TxIn GetInputFromUtxo(BitcoinSecret secret, FromUtxoBcash utxo, List<BitcoinSecret> privateKeysToSign, List<Coin> coinsToSpent)
        {
            uint256 transactionId = uint256.Parse(utxo.TxHash);
            var outpoint = new OutPoint(transactionId, (uint)utxo.Index);

            privateKeysToSign.Add(secret);
            var scriptSig = secret.GetAddress(ScriptPubKeyType.Legacy).ScriptPubKey;

            long value = (long)(Convert.ToDecimal(utxo.Value) * 100000000);

            var coinToSpent = new Coin(transactionId, (uint)utxo.Index, Money.Satoshis(value), scriptSig);
            coinsToSpent.Add(coinToSpent);

            return new TxIn(outpoint, scriptSig);
        }
    }
}
