using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class WalletInfoResponse
    {
        public BigInteger Balance
        {
            get;
            set;
        }

        public BigInteger Pending
        {
            get;
            set;
        }

        public Int64 AccountsCount
        {
            get;
            set;
        }

        public Int64 AdhocCount
        {
            get;
            set;
        }

        public Int64 DeterministicCount
        {
            get;
            set;
        }

        public Int64 DeterministicIndex
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