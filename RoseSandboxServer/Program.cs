using RoseSandboxServer.Core;
using RoseSandboxServer.Core.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer
{
    /// <summary>
    /// Main program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args">Args.</param>
        /// <returns>Task.</returns>
        static async Task Main(string[] args)
        {
            var server = new SandboxServer();

            await server.StartAsync();
        }
    }
}
