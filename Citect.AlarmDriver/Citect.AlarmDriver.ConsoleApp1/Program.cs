using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Citect.AlarmDriver.ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Citect!");

            //using (var db = new AlarmDbConnection("AlarmServer1", "127.0.0.1", 5482))
            //{
            //    var result = db.Query("SELECT * FROM CiAlarmObject");
            //    var result2 = db.QueryAsync("SELECT * FROM CiAlarmObject").Result;
            //}

            var service1 = new AlarmDbService("Alarm1", "sligo", 5002);
            var service2 = new AlarmDbService("Alarm2", "sligo", 5005);

            try
            {
                var alarms1 = await service1.GetLastAlarmsAsync(0);
                var alarms2 = await service2.GetLastAlarmsAsync(0);
            }
            catch (Exception)
            {
                //service.ChangeServer("AlarmServer1", "127.0.0.1", 5482);
                //var alarms2 = service.GetAlarmsAsync().Result;
            }


            //var alarmsState = service.GetLastAlarmsAsync().Result;
            //var events = service.GetEventJournalAsync(DateTime.Now.AddDays(-1), DateTime.Now).Result;

            //var result = Enumerable.Join(events, alarms, x => x.AlarmId, x => x.Id, (e, a) => new { e, a }).ToList();
            //var sys = events.Where(x => x.AlarmId == 0).ToList();
        }
    }
}