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
    public static partial class Configuration
    {
        /// <summary>
        /// If the server should display a lot of informations.
        /// </summary>
        public static bool Verbose = true;
        /// <summary>
        /// If the server should display every decoded packet.
        /// </summary>
        static public bool DisplayPacket = true;
        /// <summary>
        /// Server refresh rate (in ms).
        /// </summary>
        public static int ServerRefreshRate = 10;
        /// <summary>
        /// Name of the server.
        /// </summary>
        public static string ServerName = "Rose Revolution";
        /// <summary>
        /// Ping rate (in seconds).
        /// </summary>
        public static int Ping = 60;
        /// <summary>
        /// Username maximum length.
        /// </summary>
        public static int UsernameMaximumLength = 16;
    }
}
