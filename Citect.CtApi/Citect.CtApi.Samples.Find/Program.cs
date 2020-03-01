using System;

namespace Citect.CtApi.Samples.Find
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Citect!");
            try
            {
                using (var ctApi = new CtApi())
                {
                    ctApi.Open();
                    var alarms = ctApi.Find("Alarm", "TAG=BP12*", "", "TAG", "NAME", "DESC");

                    foreach (var alarm in alarms)
                    {
                        Console.WriteLine($"TAG={alarm["TAG"]}, NAME={alarm["NAME"]}, DESC={alarm["DESC"]}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
