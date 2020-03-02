using System;
using System.Data;
using System.Data.Odbc;

namespace Citect
{
    /// <summary>
    /// Citect alarm database connection using the ODBC Citect Alarm Driver
    /// </summary>
    public class AlarmDbConnection : IDbConnection
    {
        public string ConnectionString { get => connection.ConnectionString; set => connection.ConnectionString = value; }

        public int ConnectionTimeout => connection.ConnectionTimeout;

        public string Database => connection.Database;

        public ConnectionState State => connection.State;

        /// <summary>
        /// Database connection
        /// </summary>
        private readonly IDbConnection connection;

        /// <summary>
        /// Create a new Citect alarm database connection
        /// </summary>
        public AlarmDbConnection(string server, string systemsXml)
        {
            connection = new OdbcConnection($"DRIVER={{Citect Alarm Driver}};Server={server};SystemsXml={systemsXml};");
        }

        public IDbTransaction BeginTransaction()
        {
            return connection.BeginTransaction();
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return connection.BeginTransaction(il);
        }

        public void ChangeDatabase(string databaseName)
        {
            connection.ChangeDatabase(databaseName);
        }

        public void Close()
        {
            connection.Close();
        }

        public IDbCommand CreateCommand()
        {
            return connection.CreateCommand();
        }

        public void Open()
        {
            connection.Open();
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
