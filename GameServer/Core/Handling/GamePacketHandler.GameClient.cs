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
        public async Task GetGameServer(GameClient client, Packet packet)
        {
            await SendPacket(SendGameServer(), client);
        }

        public async Task GetChannels(GameClient client, Packet packet)
        {
            Console.WriteLine("received channel packets");
            await SendPacket(SendChannels(), client);
        }
    }
}
