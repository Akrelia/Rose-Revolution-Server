using RevolutionCore.Networking;
using RoseSandboxServer.Core.Data;
using RoseSandboxServer.Core.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core
{
    /// <summary>
    /// Every random generation methods used in the sandbox server.
    /// </summary>
    public partial class SandboxServer
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Generate a random position around a position.
        /// </summary>
        /// <param name="origin">Origin.</param>
        /// <param name="range">Range.</param>
        /// <returns>Random position.</returns>
        public static Vector3 RandomPosition(Vector3 origin, float range)
        {
            float offsetX = (float)(random.NextDouble() * 2 - 1) * range;
            float offsetZ = (float)(random.NextDouble() * 2 - 1) * range;

            return new Vector3(origin.x + offsetX, origin.y, origin.z + offsetZ);
        }

        public int RandomInt()
        {
            return random.Next(0, int.MaxValue);
        }
    }
}
