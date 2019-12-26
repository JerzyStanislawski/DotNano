using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class AccountsFrontiersResponse
    {
        public Dictionary<PublicAddress, HexKey64> Frontiers
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