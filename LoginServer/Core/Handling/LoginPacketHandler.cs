using RevolutionCore.Networking;
using RevolutionCore.SQL;
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
    public partial class LoginPacketHandler : PacketHandler<LoginClient>
    {
        LoginServer server;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="server">Server instance.</param>
        /// <param name="database">Current database connection instance.</param>
        public LoginPacketHandler(LoginServer server, Database database) : base(database)
        {
            this.server = server;
        }

        /// <summary>
        /// Initialize the handlings.
        /// </summary>
        public override void Initialize()
        {
         Handlings.Add(0x10, TestReply);
        }
    }
}
