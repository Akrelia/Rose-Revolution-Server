using RevolutionCore.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseLoginServer.Core.Handling
{
    /// <summary>
    /// The packet handler.
    /// </summary>
    public partial class LoginPacketHandler
    {
        /// <summary>
        /// Test reply.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="packet">Packet.</param>
        /// <returns>Task.</returns>
        public async Task TestReply(LoginClient client, Packet packet)
        {
            await SendPacket(TestPacket(210), client);
        }
    }
}
