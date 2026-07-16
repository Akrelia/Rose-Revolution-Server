using RevolutionShared.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Networking
{

    /// <summary>
    /// Interface for writing data to a packet.
    /// </summary>
    public interface IPacketWritable
    {
        /// <summary>
        /// Write data to the given packet.
        /// </summary>
        /// <param name="packet">Packet.</param>
        void WriteToPacket(PacketOut packet);
    }
}
