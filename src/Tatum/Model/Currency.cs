﻿using System;

namespace TatumPlatform.Model
{
    public enum Currency
    {
        [EnumString("BTC")]
        BTC,

        [EnumString("BCH")]
        BCH,

        [EnumString("LTC")]
        LTC,

        [EnumString("ETH")]
        ETH,

        [EnumString("XRP")]
        XRP,

        [EnumString("XLM")]
        XLM,

        [EnumString("VET")]
        VET,

        [EnumString("NEO")]
        NEO,

        [EnumString("BNB")]
        BNB,

        [EnumString("USDT")]
        USDT,

        [EnumString("LEO")]
        LEO,

        [EnumString("LINK")]
        LINK,

        [EnumString("UNI")]
        UNI,

        [EnumString("FREE")]
        FREE,

        [EnumString("MKR")]
        MKR,

        [EnumString("USDC")]
        USDC,

        [EnumString("BAT")]
        BAT,

        [EnumString("TUSD")]
        TUSD,

        [EnumString("PAX")]
        PAX,

        [EnumString("PLTC")]
        PLTC,

        [EnumString("XCON")]
        XCON,

        [EnumString("MMY")]
        MMY,

        [EnumString("PAXG")]
        PAXG,

        [EnumString("TRX")]
        TRX,

        [EnumString("WBTC")]
        WBTC,

        [EnumString("GMC")]
        GMC,

        [EnumString("GMC_BSC")]
        GMC_BSC
    }

    public static class CurrencyExtensions
    {
        public static string GetEnumString(this Currency currency)
        {
            var enumType = currency.GetType();
            var enumName = Enum.GetName(enumType, currency);
            if (enumName != null)
            {
                var field = enumType.GetField(enumName);

                return (Attribute.GetCustomAttribute(field, typeof(EnumStringAttribute)) as EnumStringAttribute).EnumString;
            }

            return null;
        }

        public static bool IsEthereumBasedCurrency(this Currency currency)
        {
            switch (currency)
            {
                case Currency.ETH:
                case Currency.USDT:
                case Currency.LEO:
                case Currency.LINK:
                case Currency.UNI:
                case Currency.FREE:
                case Currency.MKR:
                case Currency.USDC:
                case Currency.BAT:
                case Currency.TUSD:
                case Currency.PAX:
                case Currency.PLTC:
                case Currency.XCON:
                case Currency.MMY:
                case Currency.PAXG:
                case Currency.WBTC:
                case Currency.GMC:
                case Currency.GMC_BSC:
                    return true;
                default:
                    return false;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumStringAttribute : Attribute
    {
        public EnumStringAttribute(string enumString)
        {
            EnumString = enumString;
        }
        public string EnumString { get; private set; }
    }
}
