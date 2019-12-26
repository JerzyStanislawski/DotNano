using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class NodeIdResponse
    {
        public HexKey64 Private
        {
            get;
            set;
        }

        public HexKey64 Public
        {
            get;
            set;
        }

        public PublicAddress AsAccount
        {
            get;
            set;
        }

        public String NodeId
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