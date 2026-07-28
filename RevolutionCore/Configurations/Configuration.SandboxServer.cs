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
    public class SandboxConfiguration : ServerConfiguration
    {
        /// <summary>
        /// Server address.
        /// </summary>
        private static string SandboxServerAddress = "192.168.1.198";
        /// <summary>
        /// Server port.
        /// </summary>
        private static short SandboxServerPort = 27350;
        /// <summary>
        /// Server isc port.
        /// </summary>
        private static short SandboxServerPortIsc = 30010;
        /// <summary>
        /// Starting map id.
        /// </summary>  
        public int StartingMapID = 65;
        /// <summary>
        /// Max monster spawn amount.
        /// </summary>
        public int MaxMonsterSpawnAmount = 100;
        /// <summary>
        /// MOTD.
        /// </summary>
        public string MOTD = "Welcome to the Sandbox server of Rose Revolution !";

        /// <summary>
        /// Constructor.
        /// </summary>
        public SandboxConfiguration() : base(SandboxServerAddress, SandboxServerPort, SandboxServerPortIsc)
        {
        }
    }
}
