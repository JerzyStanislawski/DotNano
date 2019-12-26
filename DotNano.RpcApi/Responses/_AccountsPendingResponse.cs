using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public abstract class AccountsPendingResponseBase
    {
        public String Error
        {
            get;
            set;
        }

        public bool IsSuccessful => String.IsNullOrEmpty(Error);
    }

    public class AccountsPendingBasicResponse : AccountsPendingResponseBase
    {
        public Dictionary<PublicAddress, IEnumerable<HexKey64>> Blocks
        {
            get;
            set;
        }
    }

    public class AccountsPendingResponse : AccountsPendingResponseBase
    {
        public Dictionary<PublicAddress, Dictionary<HexKey64, AccountsPendingBlock>> Blocks
        {
            get;
            set;
        }
    }

    public class AccountsPendingBlock
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

        public static implicit operator AccountsPendingBlock(string amount)
        {
            return new AccountsPendingBlock
            {
                Amount = BigInteger.Parse(amount)
            };
        }
    }
}