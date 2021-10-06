using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatumPlatform
{
    public static class TatumHelper
    {
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
    }
}
