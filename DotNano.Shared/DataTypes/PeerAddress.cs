using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DotNano.Shared.DataTypes
{
    public class PeerAddress
    {
        public IPAddress IPAddress { get; private set; }
        public int Port { get; private set; }

        public PeerAddress(IPAddress ipAddress, int port)
        {
            IPAddress = ipAddress;
            Port = port;
        }

        public static bool TryParse(string input, out PeerAddress peerAddress)
        {
            peerAddress = null;
            try
            {
                var index = input.LastIndexOf(':');
                if (index >= 0)
                {
                    var ipAddressString = input.Substring(0, index);
                    var portString = input.Substring(index+1);

                    var ipAddress = IPAddress.Parse(ipAddressString);
                    var port = int.Parse(portString);

                    peerAddress = new PeerAddress(ipAddress, port);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return obj is PeerAddress address &&
                   EqualityComparer<IPAddress>.Default.Equals(IPAddress, address.IPAddress) &&
                   Port == address.Port;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IPAddress, Port);
        }

        public static implicit operator PeerAddress(string ip)
        {
            if (PeerAddress.TryParse(ip, out var peerAddress))
                return peerAddress;

            throw new ArgumentException($"Could not cast {ip} to PeerAddress.");
        }
    }
}
