using Citect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var trnQueryDisplayMode = DisplayMode.Get(Ordering.OldestToNewest, Condense.Mean, Stretch.Raw, 0, BadQuality.Zero, Raw.Totaly);
            //var starttime = DateTime.UtcNow.AddMinutes(-10);
            //var endtime = DateTime.UtcNow.AddMinutes(-5);

            try
            {
                //using (var ctApi = new CtApi(true, "", "", ""))
                using (var ctApi = new CtApi(true, "sligo", "amics", "bison"))
                {
                    Console.WriteLine("connected");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine(e.ToString());
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
