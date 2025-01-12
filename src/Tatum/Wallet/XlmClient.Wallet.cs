﻿using stellar_dotnet_sdk;

namespace TatumPlatform.Clients
{
    public partial class XlmClient
    {
        Wallet IXlmClient.CreateWallet(string secret)
        {
            KeyPair keypair;
            if (string.IsNullOrWhiteSpace(secret))
            {
                keypair = KeyPair.Random();
            }
            else
            {
                keypair = KeyPair.FromSecretSeed(secret);
            }

            return new Wallet
            {
                PrivateKey = keypair.SecretSeed,
                Address = keypair.Address
            };
        }
    }
}
