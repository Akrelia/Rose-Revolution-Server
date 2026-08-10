using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Configurations
{
    /// <summary>
    /// Configuration file.
    /// </summary>
    public abstract class ServerConfiguration
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serverAddress">Server address.</param>
        /// <param name="serverPort">Server port.</param>
        /// <param name="serverPortIsc">Server port isc.</param>
        public ServerConfiguration(string serverAddress, short serverPort, short serverPortIsc)
        {
            ServerAddress = serverAddress;
            ServerPort = serverPort;
            ServerPortIsc = serverPortIsc;
        }

        /// <summary>
        /// Server address.
        /// </summary>
        public string ServerAddress;
        /// <summary>
        /// Server port.
        /// </summary>
        public short ServerPort;
        /// <summary>
        /// Server isc port.
        /// </summary>
        public short ServerPortIsc;
        /// <summary>
        /// If the server should display a lot of informations.
        /// </summary>
        public bool Verbose = true;
        /// <summary>
        /// If the server should display every decoded packet.
        /// </summary>
        public bool DisplayPacket = true;
        /// <summary>
        /// Server refresh rate (in ms).
        /// </summary>
        [JsonIgnore]
        public int ServerRefreshRate = 10;
        /// <summary>
        /// Name of the server.
        /// </summary>
        public string ServerName = "Rose Revolution";
        /// <summary>
        /// Ping rate (in seconds).
        /// </summary>
        [JsonIgnore]
        public TimeSpan PingRate = TimeSpan.FromSeconds(60);
        /// <summary>
        /// Username maximum length.
        /// </summary>
        [JsonIgnore]
        public int UsernameMaximumLength = 16;
        /// <summary>
        /// Maximum packet size.
        /// </summary>
        [JsonIgnore]
        public int MaximumPacketSize = 200 * 1024;
        /// <summary>
        /// Packets per second.
        /// </summary>
        [JsonIgnore]
        public int PacketsPerSecond = 20;
        /// <summary>
        /// Ping duration.
        /// </summary>
        [JsonIgnore]
        public TimeSpan PingDuration = TimeSpan.FromSeconds(10);
        /// <summary>
        /// Check packet rate.
        /// </summary>
        [JsonIgnore]
        public TimeSpan CheckPacketRate = TimeSpan.FromSeconds(1);
        /// <summary>
        /// Path to the game data folder.
        /// </summary>
        public string GameDataPath = "GameData";
        /// <summary>
        /// Tick rate.
        /// </summary>
        [JsonIgnore]
        public TimeSpan TickRate = TimeSpan.FromMilliseconds(100);
    }
}
