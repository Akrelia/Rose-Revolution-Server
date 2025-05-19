using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionShared.Networking.Packets
{
    /// <summary>
    /// Interface Packet.
    /// </summary>
    public interface IPacket
    {
        /// <summary>
        /// Command of the packet.
        /// </summary>
        short Command { get; }
        /// <summary>
        /// Size of the packet.
        /// </summary>
        int Size { get; }
        /// <summary>
        /// Buffer of the packet.
        /// </summary>
        byte[] Buffer { get; }
        /// <summary>
        /// Packet in string format.
        /// </summary>
        string StringFormat { get; }
    }
}
