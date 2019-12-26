using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class ConfirmationHistoryResponse
    {
        public ConfirmationHistoryConfirmationStat ConfirmationStats
        {
            get;
            set;
        }

        public IEnumerable<ConfirmationHistoryConfirmation> Confirmations
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

    public class ConfirmationHistoryConfirmationStat
    {
        public Int64 Count
        {
            get;
            set;
        }

        public Int64 Average
        {
            get;
            set;
        }
    }

    public class ConfirmationHistoryConfirmation
    {
        public HexKey64 Hash
        {
            get;
            set;
        }

        public Int64 Duration
        {
            get;
            set;
        }

        public Int64 Time
        {
            get;
            set;
        }

        public BigInteger Tally
        {
            get;
            set;
        }

        public Int64 Blocks
        {
            get;
            set;
        }

        public Int64 Voters
        {
            get;
            set;
        }

        public Int64 RequestCount
        {
            get;
            set;
        }
    }
}