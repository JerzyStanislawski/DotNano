using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public abstract class RepresentativesOnlineResponseBase
    {
        public String Error
        {
            get;
            set;
        }

        public bool IsSuccessful => String.IsNullOrEmpty(Error);
    }

    public class RepresentativesOnlineBasicResponse : RepresentativesOnlineResponseBase
    {
        public IEnumerable<PublicAddress> Representatives
        {
            get;
            set;
        }
    }

    public class RepresentativesOnlineResponse : RepresentativesOnlineResponseBase
    {
        public Dictionary<PublicAddress, RepresentativesOnlineRepresentative> Representatives
        {
            get;
            set;
        }
    }

    public class RepresentativesOnlineRepresentative
    {
        public BigInteger Weight
        {
            get;
            set;
        }
    }
}