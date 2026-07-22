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
        string playerName;
        CharacterAppearance appearance;
        public int map;
        public Vector3 position;

        /// <summary>
        /// Parameterless Constructor.
        /// </summary>
        public SandboxClient() : base()
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
            return $"{(string.IsNullOrEmpty(playerName) ? base.ToString() : playerName)}";
        }
    }
}
