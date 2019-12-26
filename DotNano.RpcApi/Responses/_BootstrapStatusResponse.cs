using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;
using Newtonsoft.Json;

namespace DotNano.RpcApi.Responses
{
    public class BootstrapStatusResponse
    {
        public Int32 Clients
        {
            get;
            set;
        }

        public Int32 Pulls
        {
            get;
            set;
        }

        public Int32 Pulling
        {
            get;
            set;
        }

        public Int32 Connections
        {
            get;
            set;
        }

        public Int32 Idle
        {
            get;
            set;
        }

        public Int32 TargetConnections
        {
            get;
            set;
        }

        public Int32 TotalBlocks
        {
            get;
            set;
        }

        public Int32 RunsCount
        {
            get;
            set;
        }

        public Int32 RequeuedPulls
        {
            get;
            set;
        }

        public Boolean FrontiersReceived
        {
            get;
            set;
        }

        public Boolean FrontiersConfirmed
        {
            get;
            set;
        }

        public String Mode
        {
            get;
            set;
        }

        public Int32 LazyBlocks
        {
            get;
            set;
        }

        public Int32 LazyStateBacklog
        {
            get;
            set;
        }

        public Int32 LazyBalances
        {
            get;
            set;
        }

        public Int32 LazyDestinations
        {
            get;
            set;
        }

        public Int32 LazyUndefinedLinks
        {
            get;
            set;
        }

        public Int32 LazyPulls
        {
            get;
            set;
        }

        public Int32 LazyKeys
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "lazy_key_1")]
        public HexKey64 LazyKey1
        {
            get;
            set;
        }

        public Int32 Duration
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