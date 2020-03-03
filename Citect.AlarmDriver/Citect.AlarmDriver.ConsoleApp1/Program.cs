using Dapper;
using System;

namespace Citect.AlarmDriver.ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Citect!");

            using (var db = new AlarmDbConnection("AlarmServer1", @"C:\ProgramData\Schneider Electric\Citect SCADA 2018\User\Factory Controls\Systems.xml"))
            {
                var result = db.Query("SELECT * FROM CiAlarmObject");
            }

            using (var db = new AlarmDbConnection("AlarmServer1", "127.0.0.1", 5482))
            {
                var result = db.Query("SELECT * FROM CiAlarmObject");
            }
        }
    }
}
