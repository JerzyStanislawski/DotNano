using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class BlocksInfoResponse
    {
        public IEnumerable<HexKey64> BlocksNotFound
        {
            get;
            set;
        }

        public Dictionary<HexKey64, BlocksInfoBlock> Blocks
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

    public class BlocksInfoBlock
    {
        public PublicAddress BlockAccount
        {
            get;
            set;
        }

        public BigInteger Amount
        {
            get;
            set;
        }

        public BigInteger Balance
        {
            get;
            set;
        }

        public Nullable<Int64> Height
        {
            get;
            set;
        }

        public Nullable<Int64> LocalTimestamp
        {
            get;
            set;
        }

        public Nullable<Boolean> Confirmed
        {
            get;
            set;
        }

        public BlockContent Contents
        {
            get;
            set;
        }

        public String Subtype
        {
            get;
            set;
        }

        public Nullable<BigInteger> Pending
        {
            get;
            set;
        }

        public PublicAddress SourceAccount
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
}