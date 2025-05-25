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
        string playerName;
        public byte gender;
        public byte hair;
        public byte face;
        public int back;
        public int body;
        public int gloves;
        public int shoes;
        public int mask;
        public int hat;
        public int weapon;
        public int subweapon;

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

        /// <summary>
        /// Get or the player name.
        /// </summary>
        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }
    }
}
