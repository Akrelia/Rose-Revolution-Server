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
    public partial class Configuration
    {
        /// <summary>
        /// Server address.
        /// </summary>
        public static string WorldServerAddress = "192.168.1.198";
        /// <summary>
        /// Server port.
        /// </summary>
        public static short WorldServerPort = 29200;
        /// <summary>
        /// Server port.
        /// </summary>
        public static short WorldServerPortIsc = 29210;
        /// <summary>
        /// Welcome message when a player enter the world.
        /// </summary>
        public static string WelcomeMessage = $"Welcome to Rose Revolution";
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
    }
}
