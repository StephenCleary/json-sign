using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace Nito.NormalizedJson
{
    public static class Helpers
    {
        private static readonly Regex NumberRegex = new Regex(@"^(-)?(0|(?:[1-9][0-9]*))(?:\.([0-9]+))?(?:[Ee]([-+]?[0-9]+))?$");

        public static string NormalizeNumber(string number)
        {
            var parts = NumberRegex.Match(number);
            var sign = parts.Groups[1].Value;
            var integer = parts.Groups[2].Value;
            var fraction = parts.Groups[3].Value;
            var exponent = parts.Groups[4].Value == "" ? BigInteger.Zero : BigInteger.Parse(parts.Groups[4].Value);

            fraction = fraction.TrimEnd('0');
            while (integer == "0" && fraction != "")
            {
                integer = fraction.Substring(0, 1);
                fraction = fraction.Substring(1);
                --exponent;
            }
            while (integer.Length > 1)
            {
                if (fraction != "" || integer[integer.Length - 1] != '0')
                    fraction = integer.Substring(integer.Length - 1) + fraction;
                integer = integer.Substring(0, integer.Length - 1);
                ++exponent;
            }
            if (integer == "0")
                return "0";
            var sb = new StringBuilder();
            sb.Append(sign);
            sb.Append(integer);
            if (fraction != "")
            {
                sb.Append('.');
                sb.Append(fraction);
            }
            if (exponent != 0)
            {
                sb.Append('e');
                sb.Append(exponent.ToString("D", CultureInfo.InvariantCulture));
            }
            return sb.ToString();
        }
    }
}
