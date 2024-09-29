using RevolutionCore.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseLoginServer.Core.Handling
{
    /// <summary>
    /// Build packets.
    /// </summary>
    public partial class LoginPacketHandler
    {
        /// <summary>
        /// Test packet.
        /// </summary>
        /// <returns>Packet.</returns>
        public Packet TestPacket(int level)
        {
            Packet packet = new Packet();

            packet.Start(0x11);
            packet.Add(level);

            return packet;
        }
    }
}