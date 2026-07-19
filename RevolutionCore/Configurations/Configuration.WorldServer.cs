using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Configurations
{
    /// <summary>
    /// Configuration for the world server.
    /// </summary>
    public class WorldConfiguration : ServerConfiguration
    {
        /// <summary>
        /// Server address.
        /// </summary>
        private static string WorldServerAddress = "192.168.1.198";
        /// <summary>
        /// Server port.
        /// </summary>
        private static short WorldServerPort = 29200;
        /// <summary>
        /// Server port.
        /// </summary>
        private static short WorldServerPortIsc = 29210;
        /// <summary>
        /// Welcome message when a player enter the world.
        /// </summary>
        public string WelcomeMessage = $"Welcome to Rose Revolution";
        /// <summary>
        /// Administrator minimum right.
        /// </summary>
        public static int AdministratorRight = 700;
        /// <summary>
        /// Game master minimum right.
        /// </summary>
        public static int GameMasterRight = 300;
        /// <summary>
        /// Moderator minimum right.
        /// </summary>
        public static int ModeratorRight = 100;

        /// <summary>
        /// Constructor.
        /// </summary>
        public WorldConfiguration() : base(WorldServerAddress, WorldServerPort, WorldServerPortIsc)
        {

        }

    }
}
