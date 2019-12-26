using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class BlockCountResponse
    {
        public Int64 Count
        {
            get;
            set;
        }

        public Int64 Unchecked
        {
            get;
            set;
        }

        public Int64 Cemented
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