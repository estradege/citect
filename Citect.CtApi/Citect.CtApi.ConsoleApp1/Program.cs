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
            using (var ctApi = new CtApi(true))
            {
                //CVS_BE_1_2_P1_ACPA
                Console.WriteLine("C_AUXILIAIRES.CVS_BE_1_2_P1_ACPA");

                var trnQueryDisplayMode = DisplayMode.Get(Ordering.OldestToNewest, Condense.Mean, Stretch.Raw, 0, BadQuality.Zero, Raw.None);
                //var trends = await ctApi.TrnQueryAsync(DateTime.UtcNow.AddMinutes(-10), DateTime.UtcNow.AddMinutes(-1), 0, "C_AUXILIAIRES.CVS_BE_1_2_P1_ACPA", trnQueryDisplayMode, 1, "C_AUXILIAIRES");
               
                await TrnQueryAsync(ctApi, DateTime.UtcNow.AddMinutes(-10), DateTime.UtcNow.AddMinutes(-1), 0, "C_AUXILIAIRES.CVS_BE_1_2_P1_ACPA", trnQueryDisplayMode, 1, "C_AUXILIAIRES");

                //foreach (var trend in trends)
                //{
                //    Console.WriteLine($"{trend.DateTime} / {trend.Value}");
                //}
            }

            Console.WriteLine("C_AUXILIAIRES.CVS_BE_1_2_P1_ACPA");
            Console.ReadLine();
        }

        static async Task TrnQueryAsync(CtApi ctApi, DateTime starttime, DateTime endtime, float period, string tagName, uint displayMode, int dataMode, string cluster)
        {
            var format = new NumberFormatInfo { NumberDecimalSeparator = "." };
            var endTimeSeconds = new DateTimeOffset(endtime.ToUniversalTime()).ToUnixTimeSeconds();
            var totalSeconds = endtime.Subtract(starttime).TotalSeconds;
            var numSamples = totalSeconds / period;
            var instantTrend = 0;
            var samplePeriod = 250;

            var query = $"TRNQUERY,{endTimeSeconds},{endtime.Millisecond},{period.ToString(format)},{Math.Round(numSamples)},{tagName},{displayMode},{dataMode},{instantTrend},{samplePeriod}";
            var trends = await ctApi.FindAsync(query, null, cluster, new string[] { "DATETIME", "MSECONDS", "VALUE", "QUALITY" });

            foreach (var trend in trends)
            {
                Console.WriteLine($"{trend["DATETIME"]} / {trend["VALUE"]}");

                //DateTimeSeconds = Convert.ToInt64(x["DATETIME"]),
                //DateTimeMSeconds = Convert.ToInt32(x["MSECONDS"]),
                //Value = Convert.ToDouble(x["VALUE"]),
                //Quality = Convert.ToInt32(x["QUALITY"]),
            }
        }
    }
}
