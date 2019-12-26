using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class AccountHistoryResponse
    {
        public PublicAddress Account
        {
            get;
            set;
        }

        public HexKey64 Previous
        {
            get;
            set;
        }

        public IEnumerable<AccountHistoryHistory> History
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

    public class AccountHistoryHistory
    {
        public String Type
        {
            get;
            set;
        }

        public PublicAddress Account
        {
            get;
            set;
        }

        public BigInteger Amount
        {
            get;
            set;
        }

        public Int64 LocalTimestamp
        {
            get;
            set;
        }

        public Int64 Height
        {
            get;
            set;
        }

        public HexKey64 Hash
        {
            get;
            set;
        }
    }
}