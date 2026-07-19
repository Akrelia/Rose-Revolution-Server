using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.Configurations
{
    /// <summary>
    /// Configuration for the database.
    /// </summary>
    public static class DatabaseConfiguration
    {
        /// <summary>
        /// Database's ip.
        /// </summary>
        public static string DatabaseAddress = "192.168.1.57";
        /// <summary>
        /// Database's port.
        /// </summary>
        public static short DatabasePort = 5432;
        /// <summary>
        /// Database's name.
        /// </summary>
        public static string DatabaseName = "roserevolution";
        /// <summary>
        /// Database's SQL user.
        /// </summary>
        public static string DatabaseUser = "postgres";
        /// <summary>
        /// Database's password.
        /// </summary>
        public static string DatabasePassword = "password";
        /// <summary>
        /// Database's time out.
        /// </summary>
        public static int DatabaseTimeOut = 5;
    }
}