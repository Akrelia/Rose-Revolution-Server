using RevolutionCore.Configurations;
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

            StartUp(server.Configuration);

            await server.StartAsync();
        }

        /// <summary>
        /// Some initialization on the start up.
        /// </summary>
        static public void StartUp(ServerConfiguration configuration)
        {
            Console.Title = $"Sandbox Server ({configuration.ServerAddress}:{configuration.ServerPort})";
            Console.SetWindowSize(165, 50);
        }
    }
}
