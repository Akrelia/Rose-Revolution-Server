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
        /// Login Successfull.
        /// </summary>
        /// <returns>Packet.</returns>
        public Packet LoginSuccessfullPacket()
        {
            Packet packet = new Packet();

            packet.Start(0x11);

            return packet;
        }

        /// <summary>
        /// Login Failed.
        /// </summary>
        /// <returns>Packet.</returns>
        public Packet LoginFailedPacket(string error)
        {
            Packet packet = new Packet();

            packet.Start(0x12);
            packet.Add(error);

            return packet;
        }
    }
}