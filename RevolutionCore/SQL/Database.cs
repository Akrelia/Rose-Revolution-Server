using RevolutionCore.Utils;
using System;
using System.Collections.Generic;
using Npgsql;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

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

        /// <summary>
        /// Close the database connection.
        /// </summary>
        public void Close()
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Returns objects found in db.
        /// </summary>
        /// <typeparam name="T">The type of the result object.</typeparam>
        /// <param name="query">SQL query.</param>
        /// <param name="parameters">Query parameters (optional).</param>
        /// <returns>List of T (the result set).</returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
        {
            try
            {
                return await connection.QueryAsync<T>(query, parameters); // Utilise la version asynchrone de Dapper
            }
            catch (Exception ex)
            {
                Logger.LogFatalError($"QueryAsync failed: {ex.Message}");
                return Enumerable.Empty<T>(); // Retourne une liste vide si une erreur se produit
            }
        }

        /// <summary>
        /// Returns first object found in db
        /// </summary>
        /// <typeparam name="T">The type of the result object.</typeparam>
        /// <param name="query">SQL query.</param>
        /// <param name="parameters">Query parameters (optional).</param>
        /// <returns>List of T (the result set).</returns>
        public async Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters = null)
        {
            try
            {
                var result = await connection.QueryAsync<T>(query, parameters);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Logger.LogFatalError($"QuerySingleOrDefaultAsync failed: {ex.Message}");
                return default;
            }
        }
    }
}