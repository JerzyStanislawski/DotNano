using System;
using System.Linq;

namespace DotNano.Shared.DataTypes
{
    public class HexKey64
    {
        public const string Default = "0000000000000000000000000000000000000000000000000000000000000000";   
        public string HexKeyString { get; }

        public HexKey64(string hexKeyString)
        {
            Validate(hexKeyString);

            HexKeyString = hexKeyString;
        }

        public static bool IsHexKey64(string @string)
        {
            return @string.Length == 64 && @string.All(c => (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F'));
        }

        private void Validate(string hexKeyString)
        {
            if (!IsHexKey64(hexKeyString))
                throw new ArgumentException("Incorrect key provided");
        }

        public override string ToString()
        {
            return HexKeyString;
        }

        public override bool Equals(object obj)
        {
            return obj is HexKey64 key &&
                   HexKeyString == key.HexKeyString;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HexKeyString);
        }

        public static implicit operator HexKey64(string hexKeyString)
        {
            return new HexKey64(hexKeyString);
        }
    }
}
