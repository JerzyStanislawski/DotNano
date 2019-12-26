using System;
using System.Linq;

namespace DotNano.Shared.DataTypes
{
    public class HexKey128
    {
        public const string Default = "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
        public string HexKeyString { get; }

        public HexKey128(string hexKeyString)
        {
            Validate(hexKeyString);

            HexKeyString = hexKeyString;
        }

        public static bool IsHexKey128(string @string)
        {
            return @string.Length == 128 && @string.All(c => (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F'));
        }

        private void Validate(string hexKeyString)
        {
            if (!IsHexKey128(hexKeyString))
                throw new ArgumentException("Incorrect key provided");
        }

        public override string ToString()
        {
            return HexKeyString;
        }

        public override bool Equals(object obj)
        {
            return obj is HexKey128 key &&
                   HexKeyString == key.HexKeyString;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HexKeyString);
        }

        public static implicit operator HexKey128(string hexKeyString)
        {
            return new HexKey128(hexKeyString);
        }
    }
}
