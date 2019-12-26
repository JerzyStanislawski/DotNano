using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class WalletHistoryResponse
    {
        public IEnumerable<WalletHistoryHistory> History
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

    public class WalletHistoryHistory
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

        public PublicAddress BlockAccount
        {
            get;
            set;
        }

        public HexKey64 Hash
        {
            get;
            set;
        }

        public Int64 LocalTimestamp
        {
            get;
            set;
        }
    }
}