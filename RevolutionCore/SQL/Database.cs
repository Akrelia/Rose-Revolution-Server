using RevolutionCore.Utils;
using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevolutionCore.SQL
{
    /// <summary>
    /// Everything related to the database.
    /// </summary>
    public partial class Database
    {
        readonly string connectionString;
        readonly NpgsqlConnection connection;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="database">Database name.</param>
        /// <param name="user">User.</param>
        /// <param name="password">Password.</param>
        public Database(string dbip, string port, string database, string user, string password)
        {
            Logger.LogImportantMessage("Db infos :", $"{dbip}, {port}, {database}, {user}, {password}");
            string connectionString = $"Host={dbip};Port={port};Database={database};Username={user};Password={password};SSL Mode=Prefer;Trust Server Certificate=True;";

            connection = new NpgsqlConnection(connectionString);
        }

        /// <summary>
        /// Open the database connection.
        /// </summary>
        /// <returns>True if succesful.</returns>
        public bool Open()
        {
            try
            {
                connection.Open();

                Logger.LogImportantMessage("STARTING", $"Database connection started : [{connection.Database}]");

                return true;
            }

            catch (Exception ex)
            {
                Logger.LogFatalError($"Can't open database on SQL server : {ex.Message}");


                return false;
            }
        }
    }
}
