using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class WorkValidateResponse
    {
        public Int64 Valid
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

        public String Error
        {
            get;
            set;
        }

        public bool IsSuccessful => String.IsNullOrEmpty(Error);
    }
}