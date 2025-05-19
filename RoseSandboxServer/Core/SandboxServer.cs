using RevolutionCore.Configurations;
using RevolutionCore.Networking;
using RoseSandboxServer.Core.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core
{
    /// <summary>
    /// Sandbox server.
    /// </summary>
    public class SandboxServer : RoseServer<SandboxClient, SandboxPacketHandler>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SandboxServer() : base(Configuration.SandboxServerAddress, Configuration.SandboxServerPort, Configuration.SandboxServerAddress, Configuration.SandboxServerPortIsc)
        {
            packetHandler = new SandboxPacketHandler(this, database);
        }
    }
}
