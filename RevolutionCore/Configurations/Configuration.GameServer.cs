using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Configurations
{
    /// <summary>
    /// Configuration for the game server.
    /// </summary>
    public partial class GameConfiguration : ServerConfiguration
    {
        /// <summary>
        /// Server address.
        /// </summary>
        public static string GameServerAddress = "127.0.0.1";
        /// <summary>
        /// Server port.
        /// </summary>
        public static short GameServerPort = 29100;
        /// <summary>
        /// Server port.
        /// </summary>
        public static short GameServerPortIsc = 29110;
        /// <summary>
        /// Character delete delay (in seconds).
        /// </summary>
        public int CharacterDeleteDelay = 3600;
        /// <summary>
        /// Name of the first channel.
        /// </summary>
        public string ChannelName = "Channel [1]";

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameConfiguration() : base(GameServerAddress, GameServerPort, GameServerPortIsc)
        {
        }
    }
}
