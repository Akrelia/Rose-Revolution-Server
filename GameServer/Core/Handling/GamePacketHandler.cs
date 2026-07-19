using RevolutionCore.Networking;
using RevolutionCore.SQL;
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
    public partial class GamePacketHandler : PacketHandler<GameClient>
    {
        GameServer server;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="server">Server instance.</param>
        /// <param name="database">Database instance.</param>
        public GamePacketHandler(GameServer server, Database database) : base(server.Configuration, database)
        {
            this.server = server;
        }
    }
}
