using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class ConfirmationQuorumResponse
    {
        public BigInteger QuorumDelta
        {
            get;
            set;
        }

        public Int64 OnlineWeightQuorumPercent
        {
            get;
            set;
        }

        public BigInteger OnlineWeightMinimum
        {
            get;
            set;
        }

        public BigInteger OnlineStakeTotal
        {
            get;
            set;
        }

        public BigInteger PeersStakeTotal
        {
            get;
            set;
        }

        public BigInteger PeersStakeRequired
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