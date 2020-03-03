using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.IO;
using System.Text;

namespace Citect.AlarmDriver
{
    /// <summary>
    /// Citect alarm database connection using the ODBC Citect Alarm Driver
    /// </summary>
    public class AlarmDbConnection : DbConnection
    {
        /// <summary>
        /// Database connection
        /// </summary>
        private readonly DbConnection connection;

        public override string ConnectionString { get => connection.ConnectionString; set => connection.ConnectionString = value; }

        public override string Database => connection.Database;

        public override string DataSource => connection.DataSource;

        public override string ServerVersion => connection.ServerVersion;

        public override ConnectionState State => connection.State;

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

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return connection.BeginTransaction(isolationLevel);
        }

        public override void ChangeDatabase(string databaseName)
        {
            connection.ChangeDatabase(databaseName);
        }

        public override void Close()
        {
            connection.Close();
        }

        protected override DbCommand CreateDbCommand()
        {
            return connection.CreateCommand();
        }

        public override void Open()
        {
            connection.Open();
        }
    }
}
