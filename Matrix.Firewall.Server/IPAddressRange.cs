using System.Net;
using System.Net.Sockets;

namespace Matrix.Firewall.Server
{
    public class IPAddressRange
    {
        private readonly AddressFamily _addressFamily;

        private readonly byte[] _lowerBytes;

        private readonly byte[] _upperBytes;

        public IPAddressRange(IPAddress lowerInclusive, IPAddress upperInclusive)
        {
            _addressFamily = lowerInclusive.AddressFamily;
            _lowerBytes = lowerInclusive.GetAddressBytes();
            _upperBytes = upperInclusive.GetAddressBytes();
        }

        public bool IsInRange(IPAddress address)
        {
            var result = false;

            if (address.AddressFamily != _addressFamily)
                result = false;
            else
            {
                var addressBytes = address.GetAddressBytes();

                bool lowerBoundary = true, upperBoundary = true;

                var broken = false;

                for (int i = 0; i < _lowerBytes.Length && (lowerBoundary || upperBoundary); i++)
                {
                    if ((lowerBoundary && addressBytes[i] < _lowerBytes[i]) || (upperBoundary && addressBytes[i] > _upperBytes[i]))
                    {
                        result = false;
                        broken = true;
                        break;
                    }

                    lowerBoundary &= (addressBytes[i] == _lowerBytes[i]);
                    upperBoundary &= (addressBytes[i] == _upperBytes[i]);
                }

                if (!broken)
                    result = true;
            }

            return result;
        }
    }
}