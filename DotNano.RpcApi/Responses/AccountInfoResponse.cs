using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class AccountInfoResponse
    {
        public HexKey64 Frontier
        {
            get;
            set;
        }

        public HexKey64 OpenBlock
        {
            get;
            set;
        }

        public HexKey64 RepresentativeBlock
        {
            get;
            set;
        }

        public BigInteger Balance
        {
            get;
            set;
        }

        public Int64 ModifiedTimestamp
        {
            get;
            set;
        }

        public Int64 BlockCount
        {
            get;
            set;
        }

        public Nullable<Int64> ConfirmationHeight
        {
            get;
            set;
        }

        public Nullable<Int64> AccountVersion
        {
            get;
            set;
        }

        public PublicAddress Representative
        {
            get;
            set;
        }

        public Nullable<BigInteger> Weight
        {
            get;
            set;
        }

        public Nullable<BigInteger> Pending
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