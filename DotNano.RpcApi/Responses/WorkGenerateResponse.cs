using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class WorkGenerateResponse
    {
        public String Work
        {
            get;
            set;
        }

        public String Difficulty
        {
            get;
            set;
        }

        public Double Multiplier
        {
            get;
            set;
        }

        public HexKey64 Hash
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