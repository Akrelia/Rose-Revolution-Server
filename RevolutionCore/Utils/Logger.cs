using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Utils
{
    /// <summary>
    /// Logger class.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Log a simple message.
        /// </summary>
        /// <param name="header">Header to display.</param>
        /// <param name="text">Text to display.</param>
        public static void LogMessage(string header, string text)
        {
            WriteTime();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[{header}]{text}");
            Console.ResetColor();
        }

        /// <summary>
        /// Log a simple message.
        /// </summary>
        /// <param name="condition">Condition.</param>
        /// <param name="header">Header to display.</param>
        /// <param name="text">Text to display.</param>
        public static void LogMessage(bool condition, string header, string text)
        {
            if (condition)
            {
                WriteTime();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"[{header}]{text}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Log an important message.
        /// </summary>
        /// <param name="header">Header of the message.</param>
        /// <param name="text">Content of the message.</param>
        public static void LogImportantMessage(string header, string text)
        {
            WriteTime();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{header}] {text}");
            Console.ResetColor();
        }

        /// <summary>
        /// Log a warning.
        /// </summary>
        /// <param name="text">Text to display.</param>
        public static void LogWarning(string text)
        {
            WriteTime();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARNING] {text}");
            Console.ResetColor();
        }

        /// <summary>
        /// Log an error.
        /// </summary>
        /// <param name="text">Text to display.</param>
        public static void LogError(string text)
        {
            WriteTime();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {text}");
            Console.ResetColor();
        }

        /// <summary>
        /// Log a fatal error.
        /// </summary>
        /// <param name="text">Text to display.</param>
        public static void LogFatalError(string text)
        {
            WriteTime();

            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[FATAL ERROR] {text}");
            Console.ResetColor();
        }

        /// <summary>
        /// Write the time header.
        /// </summary>
        private static void WriteTime()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(TimeHeader);
            Console.ResetColor();
        }

        /// <summary>
        /// Get the current time in short string format.
        /// </summary>
        public static string TimeHeader
        {
            get { return $"[{DateTime.Now.ToString("dd/MM HH:mm")}]"; }
        }
    }
}
