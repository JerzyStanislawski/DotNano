using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class StatsResponse
    {
        public String Type
        {
            get;
            set;
        }

        public String Created
        {
            get;
            set;
        }

        public IEnumerable<StatsEntry> Entries
        {
            get;
            set;
        }

        public Dictionary<string, object> Node
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
    
    public class StatsEntry
    {
        public String Time
        {
            get;
            set;
        }

        public String Type
        {
            get;
            set;
        }

        public String Detail
        {
            get;
            set;
        }

        public String Dir
        {
            get;
            set;
        }

        public Int32 Value
        {
            get;
            set;
        }
    }
}