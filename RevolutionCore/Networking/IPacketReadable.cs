using RevolutionShared.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{
    /// <summary>
    /// Interface for reading data from a packet.
    /// </summary>
    public interface IPacketReadable
    {
        /// <summary>
        /// Read data from the given packet.
        /// </summary>
        /// <param name="packet">Packet.</param>
        void ReadFromPacket(PacketIn packet);
    }
}
