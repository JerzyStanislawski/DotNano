using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class WalletChangeSeedResponse
    {
        public String Success
        {
            get;
            set;
        }

        public PublicAddress LastRestoredAccount
        {
            get;
            set;
        }

        public Int64 RestoredCount
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