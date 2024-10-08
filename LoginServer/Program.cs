﻿using RevolutionCore.Configurations;
using RevolutionCore.Utils;
using RoseLoginServer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseLoginServer
{
    /// <summary>
    /// Main program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">args.</param>
        static void Main(string[] args)
        {
            StartUp();

            MainAsync().Wait();
        }

        /// <summary>
        /// Async main method.
        /// </summary>
        /// <returns>Task.</returns>
        static async Task MainAsync()
        {
            LoginServer server = new LoginServer();

            Logger.LogImportantMessage("STARTING", "Server is starting ...");

            server.Start();

            Logger.LogImportantMessage("STARTING", "Server started and listening.");

            await Task.WhenAll(server.ListenAsync(), server.UpdateAsync(), server.ListenIscAsync(), server.UpdateIscAsync());
        }

        /// <summary>
        /// Some initialization on the start up.
        /// </summary>
        static public void StartUp()
        {
            Console.Title = $"Login Server ({Configuration.LoginServerAddress}:{Configuration.LoginServerPort})";
            Console.SetWindowSize(165, 50);
        }
    }
}
