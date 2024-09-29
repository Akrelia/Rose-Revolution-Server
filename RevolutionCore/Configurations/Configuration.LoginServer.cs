using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Configurations
{
    /// <summary>
    /// Configuration for the login server.
    /// </summary>
    public partial class Configuration
    {
        /// <summary>
        /// Server address.
        /// </summary>
        public static string LoginServerAddress = "127.0.0.1";
        /// <summary>
        /// Server port.
        /// </summary>
        public static short LoginServerPort = 29000;
        /// <summary>
        /// Server port.
        /// </summary>
        public static short LoginServerPortIsc = 29010;
    }
}
