using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace DotNano.Shared.DataTypes
{
    public class BlockContent
    {
        public string Type { get; }
        public PublicAddress Account { get; }
        public HexKey64 Key { get; }
        public HexKey64 Wallet { get; }
        public HexKey64 Previous { get; }
        public HexKey64 Link { get; }
        public PublicAddress LinkAsAccount { get; }
        public PublicAddress Source { get; }
        public PublicAddress Destination { get; }
        public PublicAddress Representative { get; }
        public BigInteger Balance { get; }
        public HexKey128 Signature { get; }
        public string Work { get; }

    }
}
