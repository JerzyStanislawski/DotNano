using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public abstract class PendingResponseBase
    {
        public String Error
        {
            get;
            set;
        }

        public bool IsSuccessful => String.IsNullOrEmpty(Error);
    }

    public class PendingBasicResponse : PendingResponseBase
    {
        public IEnumerable<HexKey64> Blocks
        {
            get;
            set;
        }
    }

    public class PendingResponse : PendingResponseBase
    {
        public Dictionary<HexKey64, PendingBlock> Blocks
        {
            get;
            set;
        }
    }

    public class PendingBlock
    {
        public BigInteger Amount
        {
            get;
            set;
        }

        public PublicAddress Source
        {
            get;
            set;
        }


        public static implicit operator PendingBlock(string amount)
        {
            return new PendingBlock
            {
                Amount = BigInteger.Parse(amount)
            };
        }
    }
}