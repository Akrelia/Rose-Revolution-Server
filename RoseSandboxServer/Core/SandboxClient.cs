using RevolutionCore.Networking;
using RevolutionCore.Utils;
using RevolutionShared.Data;
using RevolutionShared.Rose.Data;
using RoseSandboxServer.Core.Data;
using RoseSandboxServer.Networking.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer
{
    /// <summary>
    /// Sandbox client.
    /// </summary>
    public class SandboxClient : RoseClient
    {
        public int map;
        public Player player;

        /// <summary>
        /// Parameterless Constructor.
        /// </summary>
        public SandboxClient() : base()
        {
            player = new Player((int)id, EntityType.Player, 0, null); // TODO : remove this asap
        }

        /// <summary>
        /// Update the world client.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Task.</returns>
        public async Task Update(TickContext context)
        {
            // Put here the logic to update the client itself later

            await player.Update(context);
        }

        /// <summary>
        /// String format.
        /// </summary>
        /// <returns>Object in string format.</returns>
        public override string ToString()
        {
            return account != null ? account.username : base.ToString();
        }
    }
}
