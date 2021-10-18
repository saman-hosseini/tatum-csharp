using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Newtonsoft.Json;

namespace TatumPlatform
{
    public static class TatumHelper
    {
        private const decimal DecimalPrecision6 = 1000000M;
        private const decimal DecimalPrecision8 = 100000000M;
        private const decimal DecimalPrecision9 = 1000000000M;
        private const decimal DecimalPrecision18 = 1000000000000000000M;
        private const long LongPrecision6 = 1000000;
        private const long LongPrecision8 = 100000000;
        private const long LongPrecision9 = 1000000000;
        private const long LongPrecision18 = 1000000000000000000;
        public const int RandomStep = 1000;

        public static T DeserializeObject<T>(string item)
        {
            var output = JsonConvert.DeserializeObject<T>(item);
            return output;
        }
        public static BigInteger ToBig(string str)
        {
            BigInteger defaultValue = new(0);
            if (String.IsNullOrEmpty(str))
                return defaultValue;
            BigInteger.TryParse(str, out defaultValue);
            return defaultValue;
        }

        public static string ToFormat(string str, int precision)
        {
            var l = str.Length;
            if (l > precision)
            {
                str = str.Insert(l - precision, ".");
            }
            else
            {
                string lz = "";
                for (int i = l; i < precision; i++)
                {
                    lz += "0";
                }
                str = "0." + lz + str;
            }
            return str;
        }

        public static decimal ToDecimal(string str, decimal defaultValue = 0)
        {
            if (String.IsNullOrEmpty(str))
                return defaultValue;
            decimal.TryParse(str, out defaultValue);
            return defaultValue;
        }

        public static long ToLong(string str, long defaultValue = 0)
        {
            if (String.IsNullOrEmpty(str))
                return defaultValue;
            long.TryParse(str, out defaultValue);
            return defaultValue;
        }

        public static decimal ToDecimal(long amount, Precision precision)
        {
            switch (precision)
            {
                case Precision.Precision6:
                    return amount / TatumHelper.DecimalPrecision6;
                case Precision.Precision8:
                    return amount / TatumHelper.DecimalPrecision8;
                case Precision.Gwei:
                    return amount / TatumHelper.DecimalPrecision9;
                case Precision.Precision18:
                    return amount / TatumHelper.DecimalPrecision18;
                default:
                    break;
            }
            throw new Exception("wrong precision for convert");
        }

        public static decimal ToDecimal(long amount, int precision)
        {
            return amount / Convert.ToDecimal(Math.Pow(10, precision));
        }

        public static long ToLong(decimal amount, Precision precision)
        {
            switch (precision)
            {
                case Precision.Precision6:
                    return decimal.ToInt64(amount * TatumHelper.LongPrecision6);
                case Precision.Precision8:
                    return decimal.ToInt64(amount * TatumHelper.LongPrecision8);
                case Precision.Gwei:
                    return decimal.ToInt64(amount * TatumHelper.LongPrecision9);
                case Precision.Precision18:
                    return decimal.ToInt64(amount * TatumHelper.LongPrecision18);
                default:
                    break;
            }
            throw new Exception("wrong precision for convert");
        }
    }

    public enum Precision
    {
        Precision6,
        Precision8,
        Precision18,
        Gwei
    }
}
