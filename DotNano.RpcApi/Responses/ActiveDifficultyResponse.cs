using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class ActiveDifficultyResponse
    {
        public String NetworkMinimum
        {
            get;
            set;
        }

        public String NetworkCurrent
        {
            get;
            set;
        }

        public Double Multiplier
        {
            get;
            set;
        }

        public IEnumerable<Double> DifficultyTrend
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