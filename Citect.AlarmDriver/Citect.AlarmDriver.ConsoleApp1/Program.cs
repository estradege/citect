using Dapper;
using System;

namespace Citect.AlarmDriver.ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Citect!");

            //using (var db = new AlarmDbConnection("AlarmServer1", "127.0.0.1", 5482))
            //{
            //    var result = db.Query("SELECT * FROM CiAlarmObject");
            //}

            var service = new AlarmDbService
            {
                Server = "AlarmServer1",
                Ip = "127.0.0.1",
                Port = 5482
            };

            var alarms = service.GetAlarms();
        }
    }
}
