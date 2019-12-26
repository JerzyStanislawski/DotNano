using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class BlockInfoResponse
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

        public Int64 Height
        {
            get;
            set;
        }

        public Int64 LocalTimestamp
        {
            get;
            set;
        }

        public Boolean Confirmed
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

        public String Error
        {
            get;
            set;
        }

        public bool IsSuccessful => String.IsNullOrEmpty(Error);
    }
}