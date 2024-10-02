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
        readonly GameServer server;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="server">Server instance.</param>
        /// <param name="database">Database instance.</param>
        public GamePacketHandler(GameServer server, Database database) : base(database)
        {
            this.server = server;
        }
        /// <summary>
        /// Initialize the handlings.
        /// </summary>
        public override void Initialize()
        {
           // Handlings.Add(Commands.CreateCharacter, CreateCharacter);
        }
    }
}
