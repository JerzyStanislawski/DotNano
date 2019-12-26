using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class VersionResponse
    {
        public Int64 RpcVersion
        {
            get;
            set;
        }

        public Int64 StoreVersion
        {
            get;
            set;
        }

        public Int64 ProtocolVersion
        {
            get;
            set;
        }

        public String NodeVendor
        {
            get;
            set;
        }

        public String StoreVendor
        {
            get;
            set;
        }

        public String Network
        {
            get;
            set;
        }

        public HexKey64 NetworkIdentifier
        {
            get;
            set;
        }

        public String BuildInfo
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