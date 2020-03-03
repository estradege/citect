using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using System.Linq;

namespace Citect.AlarmDriver
{
    /// <summary>
    /// Citect alarm database service
    /// </summary>
    public class AlarmDbService
    {
        /// <summary>
        /// Citect alarm server name
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Citect alarm server ip address
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Citect alarm server port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Looging service
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
        public AlarmDbService(ILogger<AlarmDbService> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Get all alarm objects
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Alarm> GetAlarms()
        {
            using (var db = new AlarmDbConnection(Server, Ip, Port))
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
AlarmLastUpdateTime as ""UpdateTime"",
ConfigTime
from CiAlarmObject";

                logger.LogInformation($"GetAlarms: sql={sql}");
                var alarms = db.Query<Alarm>(sql);
                logger.LogDebug($"GetAlarms: alarms.Count={alarms.Count()}");

                return alarms;
            }
        }
    }
}
