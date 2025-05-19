using RevolutionCore.Networking;
using RevolutionCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer
{
    /// <summary>
    /// Sandbox client.
    /// </summary>
    public class SandboxClient : RoseClient
    {
        /// <summary>
        /// Parameterless Constructor.
        /// </summary>
        public SandboxClient() : base()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SandboxClient(TcpClient tcpClient) : base(tcpClient)
        {
        }
    }
}
