using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Configurations
{
    /// <summary>
    /// Configuration for the sandbox server.
    /// </summary>
    public partial class Configuration
    {
        /// <summary>
        /// Server address.
        /// </summary>
        public static string SandboxServerAddress = "127.0.0.1";
        /// <summary>
        /// Server port.
        /// </summary>
        public static short SandboxServerPort = 30000;
        /// <summary>
        /// Server isc port.
        /// </summary>
        public static short SandboxServerPortIsc = 30010;
    }
}
