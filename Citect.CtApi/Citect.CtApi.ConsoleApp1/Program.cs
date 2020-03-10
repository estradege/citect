using Citect;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctApi = new CtApi();
            ctApi.Open();

            var trends = ctApi.TrnQuery(
                endtime: new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
                endtimeMs: 0,
                period: 0,
                numSamples: 25,
                tagName: "LOOP_1_PV",
                //displayMode: DisplayMode.Get(),
                displayMode: 256,
                dataMode: 1,
                instantTrend: 0,
                samplePeriod: 250,
                cluster: "Cluster1");
        }
    }
}
