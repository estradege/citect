using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Citect.AlarmDriver
{
    /// <summary>
    /// Citect alarm database service
    /// </summary>
    public class AlarmDbService : IDisposable
    {
        /// <summary>
        /// Database connectioon
        /// </summary>
        private readonly AlarmDbConnection db = new AlarmDbConnection();

        /// <summary>
        /// Logging service
        /// </summary>
        private readonly ILogger<AlarmDbService> logger;

        /// <summary>
        /// Create a new Citect alarm database service
        /// </summary>
        public AlarmDbService()
        {
        }

        /// <summary>
        /// Create a new Citect alarm database service
        /// </summary>
        public AlarmDbService(string server, string ip, int port = 5482)
        {
            db.SetConnectionString(server, ip, port);
        }

        /// <summary>
        /// Create a new Citect alarm database service
        /// </summary>
        public AlarmDbService(string server, string systemsXml)
        {
            db.SetConnectionString(server, systemsXml);
        }

        /// <summary>
        /// Create a new Citect alarm database service
        /// </summary>
        public AlarmDbService(IConfiguration config, ILogger<AlarmDbService> logger)
        {
            this.logger = logger;
            
            if (!int.TryParse(config["Citect:AlarmDbConnection:Port"], out var port))
                port = 5482;
            
            db.SetConnectionString(
                server: config["Citect:AlarmDbConnection:Server"],
                ip: config["Citect:AlarmDbConnection:Ip"],
                port: port);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            db?.Close();
            db?.Dispose();
        }

        /// <summary>
        /// Switch the connection to another server
        /// </summary>
        /// <param name="server"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void ChangeServer(string server, string ip, int port = 5482)
        {
            db.SetConnectionString(server, ip, port);
        }

        /// <summary>
        /// Get all alarm objects
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Alarm>> GetAlarmsAsync()
        {
            var sql = @"select 
Id,
AlarmSource as ""Tag"",
DisplayName as ""Name"",
Description as ""Desc"", 
AlarmCategory as ""Category"",
HelpPage as ""Help"",
Comment,
CustomFilters[0] as ""Custom1"",
CustomFilters[1] as ""Custom2"",
CustomFilters[2] as ""Custom3"",
CustomFilters[3] as ""Custom4"",
CustomFilters[4] as ""Custom5"",
CustomFilters[5] as ""Custom6"",
CustomFilters[6] as ""Custom7"",
CustomFilters[7] as ""Custom8"",
Historian,
Equipment as ""Equip"",
Name as ""Item"",
AlarmState as ""State"",
AckTime,
OnTime,
OffTime,
DisableTime,
AlarmLastUpdateTime as ""UpdateTime"",
ConfigTime
from CiAlarmObject";

            logger?.LogTrace($"Citect.AlarmDriver.AlarmDbService > GetAlarmsAsync, sql={sql}");
            var alarms = await db.QueryAsync<Alarm>(sql);
            logger?.LogDebug($"Citect.AlarmDriver.AlarmDbService > GetAlarmsAsync, sql={sql}, alarms.Count={alarms.Count()}");

            return alarms;
        }

        /// <summary>
        /// Get last change alarm state objects
        /// </summary>
        /// <param name="wimdow">Time window in seconds (0 = all alarms)</param>
        /// <returns></returns>
        public async Task<IEnumerable<AlarmState>> GetLastAlarmsAsync(uint wimdow = 5)
        {
            var ts = DateTime.UtcNow.AddSeconds(-1 * wimdow).ToString("yyyy-MM-dd HH:mm:ss");
            var sql = $@"select 
Id,
AlarmSource as ""Tag"",
AlarmState as ""State"",
AckTime,
OnTime,
OffTime,
DisableTime
from CiAlarmObject";

            if (wimdow > 0)
            {
                sql += $" where AlarmLastUpdateTime>={{ts '{ts}'}} or ConfigTime>={{ts '{ts}'}}";
            }

            logger?.LogTrace($"Citect.AlarmDriver.AlarmDbService > GetLastAlarmsAsync, wimdow={wimdow}, sql={sql}");
            var alarms = await db.QueryAsync<AlarmState>(sql);
            logger?.LogDebug($"Citect.AlarmDriver.AlarmDbService > GetLastAlarmsAsync, wimdow={wimdow}, sql={sql}, alarms.Count={alarms.Count()}");

            return alarms;
        }

        /// <summary>
        /// Get the event journal
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Event>> GetEventJournalAsync(DateTime starttime, DateTime endtime)
        {
            var sql = $@"select 
Id as ""AlarmId"",
RecordTime,
AlarmStateDesc,
Message,
Category,
User,
ClientName
from CDBEventJournal
where (RecordTime between {{ts '{starttime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}'}} and {{ts '{endtime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss")}'}})";

            logger?.LogDebug($"Citect.AlarmDriver.AlarmDbService > GetEventJournalAsync, starttime={starttime}, endtime={endtime}, sql={sql}");
            var events = await db.QueryAsync<Event>(sql);
            logger?.LogDebug($"Citect.AlarmDriver.AlarmDbService > GetEventJournalAsync, starttime={starttime}, endtime={endtime}, sql={sql}, events.Count={events.Count()}");

            return events;
        }
    }
}
