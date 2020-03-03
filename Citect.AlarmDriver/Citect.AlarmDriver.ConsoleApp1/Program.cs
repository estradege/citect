using Dapper;
using System;

namespace Citect.AlarmDriver.ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Citect!");

            using (var db = new AlarmDbConnection("AlarmServer1", "127.0.0.1", 5482))
            {
                var result = db.Query("SELECT * FROM CiAlarmObject");
                var result2 = db.QueryAsync("SELECT * FROM CiAlarmObject").Result;
            }

            var service = new AlarmDbService("AlarmServer1",  "127.0.0.1", 5482);
            var alarms = service.GetAlarmsAsync().Result;
        }
    }
}


// var result = await odbc.QueryAsync($"select Equipment,AlarmSource,AckTime,OnTime,OffTime,DisableTime,AlarmState from CiAlarmObject where ({alarmCategoryFilter}){lastUpdateTimeFilter}");

/*
 * Value = new AlarmValue
    {
        Ack = x.AlarmState == 0 || x.AlarmState == 2,
        AckTime = ConvertFromSqlTimestamp(x.AckTime),
        Dis = x.AlarmState == 1,
        DisTime = ConvertFromSqlTimestamp(x.DisableTime),
        OffTime = ConvertFromSqlTimestamp(x.OffTime),
        On = x.AlarmState == 2 || x.AlarmState == 4,
        OnTime = ConvertFromSqlTimestamp(x.OnTime),
    }
 */
