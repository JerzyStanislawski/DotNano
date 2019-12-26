using DotNano.Shared.DataTypes;
using System;
using System.Linq;
using System.Numerics;

namespace DotNano.Shared
{
    public static class Tools
    {
        public static string AlphaNumOnly(string imput)
        {
            var arr = imput.ToCharArray();

            arr = Array.FindAll(arr, (c => (char.IsLetterOrDigit(c))));
            return new string(arr);
        }

        public static string ToValidJson(string json)
        {
            return json
                .Replace("\"...\",", "")
                .Replace("...", "")
                .Trim();
        }

        public static string ReplaceFirstCharacterToLowerVariant(string name)
        {
            if (String.IsNullOrEmpty(name))
                return name;

            return Char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

        public static string ToPascalCase(string input)
        {
            var words = input.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(word => word.Substring(0, 1).ToUpper() +
                                         word.Substring(1).ToLower());

            var result = String.Concat(words);
            return result;
        }

        public static Type TypeFromValue(string value)
        {
            if (Boolean.TryParse(value, out _))
                return typeof(bool);
            else if (HexKey64.IsHexKey64(value))
                return typeof(HexKey64);
            else if (HexKey128.IsHexKey128(value))
                return typeof(HexKey128);
            else if (Int64.TryParse(value, out _))
                return typeof(long);
            else if (BigInteger.TryParse(value, out _))
                return typeof(BigInteger);
            else if (Double.TryParse(value, out _))
                return typeof(double);
            if (PublicAddress.IsPublicAddress(value))
                return typeof(PublicAddress);
            else 
                return typeof(string);
        }

        public static object TypedValueFromStringValue(string value)
        {
            if (Boolean.TryParse(value, out var boolValue))
                return boolValue;
            else if (HexKey64.IsHexKey64(value))
                return new HexKey64(value);
            else if (HexKey128.IsHexKey128(value))
                return new HexKey128(value);
            else if (Int64.TryParse(value, out var longValue))
                return longValue;
            else if (BigInteger.TryParse(value, out var bigIntValue))
                return bigIntValue;
            else if (Double.TryParse(value, out var doubleValue))
                return doubleValue;
            if (PublicAddress.IsPublicAddress(value))
                return new PublicAddress(value);
            else
                return value;
        }
    }
}
