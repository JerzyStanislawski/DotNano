using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class BlockCountTypeResponse
    {
        public Int64 Send
        {
            get;
            set;
        }

        public Int64 Receive
        {
            get;
            set;
        }

        public Int64 Open
        {
            get;
            set;
        }

        public Int64 Change
        {
            get;
            set;
        }

        public Int64 StateV0
        {
            get;
            set;
        }

        public Int64 StateV1
        {
            get;
            set;
        }

        public Int64 State
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