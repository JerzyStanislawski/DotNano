using System;
using System.Collections.Generic;
using System.Numerics;
using DotNano.Shared.DataTypes;

namespace DotNano.RpcApi.Responses
{
    public class PeersResponse
    {
        public Dictionary<PeerAddress, PeersPeer> Peers
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

    public class PeersPeer
    {
        public long ProtocolVersion
        {
            get;
            set;
        }

        public String NodeId
        {
            get;
            set;
        }

        public String Type
        {
            get;
            set;
        }

        public static implicit operator PeersPeer(string protocloVersion)
        {
            return new PeersPeer
            {
                ProtocolVersion = long.Parse(protocloVersion)
            };
        }
    }
}