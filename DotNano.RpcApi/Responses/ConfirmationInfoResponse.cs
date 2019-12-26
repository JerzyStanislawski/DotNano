using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class ConfirmationInfoResponse
    {
        public Int64 Announcements
        {
            get;
            set;
        }

        public Nullable<Int64> Voters
        {
            get;
            set;
        }

        public HexKey64 LastWinner
        {
            get;
            set;
        }

        public BigInteger TotalTally
        {
            get;
            set;
        }

        public Dictionary<HexKey64, ConfirmationInfoBlock> Blocks
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

    public class ConfirmationInfoBlock
    {
        public BigInteger Tally
        {
            get;
            set;
        }

        public BlockContent Contents
        {
            get;
            set;
        }

        public Dictionary<PublicAddress, BigInteger> Representatives
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