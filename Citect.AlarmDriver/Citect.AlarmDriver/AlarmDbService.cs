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
    public class AlarmDbService
    {
        /// <summary>
        /// Logging service
        /// </summary>
        private readonly ILogger<AlarmDbService> logger;

        /// <summary>
        /// Database connectioon
        /// </summary>
        private readonly AlarmDbConnection db;

        /// <summary>
        /// Create a new Citect alarm database service
        /// </summary>
        public AlarmDbService(string server, string ip, int port, ILogger<AlarmDbService> logger = null)
        {
            db = new AlarmDbConnection(server, ip, port);
        }

        /// <summary>
        /// Create a new Citect alarm database service
        /// </summary>
        public AlarmDbService(IConfiguration config, ILogger<AlarmDbService> logger)
        {
            this.logger = logger;
            var server = config["AlarmDbConnection:Server"];
            var ip = config["AlarmDbConnection:Ip"];
            var port = config["AlarmDbConnection:Port"];
            db = new AlarmDbConnection(server, ip, Convert.ToInt32(port));
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

            logger?.LogInformation($"GetAlarmsAsync: sql={sql}");
            var alarms = await db.QueryAsync<Alarm>(sql);
            logger?.LogDebug($"GetAlarmsAsync: alarms.Count={alarms.Count()}");

            return alarms;
        }

        /// <summary>
        /// Get last change alarm state objects
        /// </summary>
        /// <param name="wimdow">Time window in seconds</param>
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
from CiAlarmObject
where AlarmLastUpdateTime>={{ts '{ts}'}} or ConfigTime>={{ts '{ts}'}}";

            logger?.LogInformation($"GetLastAlarmsAsync: sql={sql}");
            var alarms = await db.QueryAsync<AlarmState>(sql);
            logger?.LogDebug($"GetLastAlarmsAsync: alarms.Count={alarms.Count()}");

            return alarms;
        }
    }
}

/*
//var sql = "select 
CiAlarmObject.Equipment,
CiAlarmObject.AlarmSource,
CiAlarmObject.AlarmCategory,
Historic.CDBEventJournal.RecordTime,
Historic.CDBEventJournal.AlarmStateDesc,
Historic.CDBEventJournal.Message,
Historic.CDBEventJournal.ClientName,
Historic.CDBEventJournal.User";
//sql += " from Historic.CDBEventJournal";
//                    sql += " join Citect.CiAlarmObject on Citect.CiAlarmObject.Id=Historic.CDBEventJournal.Id";
//                 // sql += " where Historic.CDBEventJournal.AlarmStateDesc<>''";
//                    sql += $" where (Historic.CDBEventJournal.RecordTime between {{ts '{utcFrom.ToString("yyyy-MM-dd HH:mm:ss")}'}} and {{ts '{utcTo.ToString("yyyy-MM-dd HH:mm:ss")}'}})";
//                    sql += $" and ({alarmCategoryFilter})";

    */