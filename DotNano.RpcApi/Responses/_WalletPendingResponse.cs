using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public abstract class WalletPendingResponseBase
    {
        public String Error
        {
            get;
            set;
        }

        public bool IsSuccessful => String.IsNullOrEmpty(Error);
    }

    public class WalletPendingBasicResponse : WalletPendingResponseBase
    {
        public Dictionary<PublicAddress, IEnumerable<HexKey64>> Blocks
        {
            get;
            set;
        }
    }

    public class WalletPendingResponse : WalletPendingResponseBase
    {
        public Dictionary<PublicAddress, Dictionary<HexKey64, WalletPendingBlock>> Blocks
        {
            get;
            set;
        }
    }

    public class WalletPendingBlock
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

        public static implicit operator WalletPendingBlock(string amount)
        {
            return new WalletPendingBlock
            {
                Amount = BigInteger.Parse(amount)
            };
        }
    }
}