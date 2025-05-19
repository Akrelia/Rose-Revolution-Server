using RevolutionCore.Networking;
using RevolutionShared.Networking.Packets;
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
        /// Login Successfull.
        /// </summary>
        /// <returns>Packet.</returns>
        public PacketOut LoginSuccessfullPacket()
        {
            PacketOut packet = new PacketOut(0x11);

            return packet;
        }

        /// <summary>
        /// Login Failed.
        /// </summary>
        /// <returns>Packet.</returns>
        public PacketOut LoginFailedPacket(string error)
        {
            PacketOut packet = new PacketOut(0x12);

            packet.Add(error);

            return packet;
        }
    }
}