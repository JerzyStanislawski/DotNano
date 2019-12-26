using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class UncheckedKeysResponse
    {
        public IEnumerable<UncheckedKeysUnchecked> Unchecked
        {
            get;
            set;
        }

        public String Error
        {
            get;
            set;
        }

        public bool IsSuccessful => String.IsNullOrEmpty(Error);
    }

    public class UncheckedKeysUnchecked
    {
        public HexKey64 Key
        {
            get;
            set;
        }

        public HexKey64 Hash
        {
            get;
            set;
        }

        public Int64 ModifiedTimestamp
        {
            get;
            set;
        }

        public BlockContent Contents
        {
            get;
            set;
        }
    }
}