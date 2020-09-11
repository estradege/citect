using Microsoft.Extensions.Configuration;
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
        #region DbConnection

        public override string ConnectionString { get => connection.ConnectionString; set => connection.ConnectionString = value; }

        public override string Database => connection.Database;

        public override string DataSource => connection.DataSource;

        public override string ServerVersion => connection.ServerVersion;

        public override ConnectionState State => connection.State;

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel) => connection.BeginTransaction(isolationLevel);

        public override void ChangeDatabase(string databaseName) => connection.ChangeDatabase(databaseName);

        public override void Close() => connection.Close();

        protected override DbCommand CreateDbCommand() => connection.CreateCommand();

        public override void Open() => connection.Open();

        #endregion

        /// <summary>
        /// Database connection
        /// </summary>
        private readonly DbConnection connection = new OdbcConnection();

        /// <summary>
        /// Create a new Citect alarm database connection
        /// </summary>
        public AlarmDbConnection()
        {
        }

        /// <summary>
        /// Create a new Citect alarm database connection
        /// </summary>
        public AlarmDbConnection(string server, string ip, int port = 5482)
        {
            SetConnectionString(server, ip, port);
        }

        /// <summary>
        /// Create a new Citect alarm database connection
        /// </summary>
        public AlarmDbConnection(string server, string systemsXml)
        {
            SetConnectionString(server, systemsXml);
        }

        /// <summary>
        /// Create a new Citect alarm database connection
        /// </summary>
        public AlarmDbConnection(IConfiguration config)
        {
            if (!int.TryParse(config["Citect:AlarmDbConnection:Port"], out var port))
                port = 5482;
            
            SetConnectionString(
                server: config["Citect:AlarmDbConnection:Server"],
                ip: config["Citect:AlarmDbConnection:Ip"],
                port: port);
        }

        /// <summary>
        /// Définit la connectionstring de la <see cref="DbConnection"/>
        /// </summary>
        /// <param name="server"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void SetConnectionString(string server, string ip, int port = 5482)
        {
            var xmlFile = new FileInfo($@"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\CitectAlarmDriver\{server}.auto.xml");
            var xmlContents = $@"<?xml version=""1.0"" encoding=""UTF-16""?>
<Systems>
    <System name=""{server}"" type=""SCX"" enabled=""true"" visibleInViewX=""true"" clientLicensing=""false"" defaultSystemPriority=""10"">
        <Server name=""{ip}"" cost=""1"" port=""{port}"" compress=""true"" connectTimeout=""30000"" requestTimeout=""120000"" disconnectTimeout=""30000"" disconnectFailedTimeout=""500"" pollInterval=""10"" pollTimeout=""15000""/>
    </System>
</Systems>";

            xmlFile.Directory.Create();
            File.WriteAllText(xmlFile.FullName, xmlContents, Encoding.Unicode);
            connection.ConnectionString = $"DRIVER={{Citect Alarm Driver}};Server={server};SystemsXml={xmlFile.FullName};";
        }

        /// <summary>
        /// Définit la connectionstring de la <see cref="DbConnection"/>
        /// </summary>
        /// <param name="server"></param>
        /// <param name="systemsXml"></param>
        public void SetConnectionString(string server, string systemsXml)
        {
            connection.ConnectionString = $"DRIVER={{Citect Alarm Driver}};Server={server};SystemsXml={systemsXml};";
        }
    }
}
