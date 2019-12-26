using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class DatabaseTxnTrackerResponse
    {
        public IEnumerable<DatabaseTxnTrackerTxnTracking> TxnTracking
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

    public class DatabaseTxnTrackerTxnTrackingStacktrace
    {
        public String Name
        {
            get;
            set;
        }

        public String Address
        {
            get;
            set;
        }

        public String SourceFile
        {
            get;
            set;
        }

        public Int64 SourceLine
        {
            get;
            set;
        }
    }

    public class DatabaseTxnTrackerTxnTracking
    {
        public String Thread
        {
            get;
            set;
        }

        public Int64 TimeHeldOpen
        {
            get;
            set;
        }

        public Boolean Write
        {
            get;
            set;
        }

        public IEnumerable<DatabaseTxnTrackerTxnTrackingStacktrace> Stacktrace
        {
            get;
            set;
        }
    }
}