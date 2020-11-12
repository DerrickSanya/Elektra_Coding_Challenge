
using System;
using System.Data;
using System.Data.SqlClient;

namespace Appointments.Infrastructure.Database
{
    /// <summary>
    /// Sql Connection Manager
    /// </summary>
    public class SqlConnectionManager : ISqlConnectionManager, IDisposable
    {
        /// <summary>
        /// connString
        /// </summary>
        private readonly string connString;

        /// <summary>
        /// connection
        /// </summary>
        private IDbConnection _dbConnection;

        /// <summary>
        /// SqlConnectionManager
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlConnectionManager(string connectionString)
        {
            connString = connectionString;
        }

        /// <summary>
        /// Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection DbConnection()
        {
            if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)
            {
                _dbConnection = new SqlConnection(connString);
                _dbConnection.Open();
            }

            return _dbConnection;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Dispose();
            }
        }
    }
}
