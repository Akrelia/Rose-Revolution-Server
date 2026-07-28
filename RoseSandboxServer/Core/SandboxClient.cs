using RevolutionCore.Networking;
using RevolutionCore.Utils;
using RevolutionShared.Data;
using RoseSandboxServer.Core.Data;
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
        public int map;
        public WorldPosition position;
        CharacterAppearance appearance;

        /// <summary>
        /// Parameterless Constructor.
        /// </summary>
        public SandboxClient() : base()
        {
        }

        /// <summary>
        /// Get or set the player appearance.
        /// </summary>
        public CharacterAppearance Appearance
        {
            get { return appearance; }
            set { appearance = value; }
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
