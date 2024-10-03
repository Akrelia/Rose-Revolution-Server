using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseGameServer.Core.Handling
{
    /// <summary>
    /// Packet handler.
    /// </summary>
    public partial class GamePacketHandler
    {
        public Packet SendGameServer()
        {
            Packet packet = new Packet();

            packet.Start(0x14);
            packet.Add(Configuration.ServerName);

            return packet;
        }

        public Packet SendChannels()
        {
            Console.WriteLine("send channels : " + Configuration.ChannelName);
            Packet packet = new Packet();

            packet.Start(0x16);
            packet.Add("Channel [1]");

            return packet;
        }
    }
}
