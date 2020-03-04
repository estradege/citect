using Dapper;
using System;
using System.Linq;

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

            var service = new AlarmDbService("AlarmServer1", "127.0.0.1", 5482);
            var alarms = service.GetAlarmsAsync().Result;
            var alarmsState = service.GetLastAlarmsAsync().Result;
        }
    }
}