using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Text;

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
        public AlarmDbConnection(string server, string ip, int port)
        {
            var xmlPath = $"{server}.auto.xml";
            var xmlContents = $@"<?xml version=""1.0"" encoding=""UTF-16""?>
<Systems>
    <System name=""{server}"" type=""SCX"" enabled=""true"" visibleInViewX=""true"" clientLicensing=""false"" defaultSystemPriority=""10"">
        <Server name=""{ip}"" cost=""1"" port=""{port}"" compress=""true"" connectTimeout=""30000"" requestTimeout=""120000"" disconnectTimeout=""30000"" disconnectFailedTimeout=""500"" pollInterval=""10"" pollTimeout=""15000""/>
    </System>
</Systems>";

            File.WriteAllText(xmlPath, xmlContents, Encoding.Unicode);
            connection = new OdbcConnection($"DRIVER={{Citect Alarm Driver}};Server={server};SystemsXml={xmlPath};");
        }

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
