using Citect;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctApi = new CtApi();
            ctApi.Open();

            var displayMode = DisplayMode.Get(Ordering.OldestToNewest, Condense.Mean, Stretch.Step, 0, BadQuality.Zero, Raw.None);

            //var trends = ctApi
            //    .TrnQuery(
            //        endtime: new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
            //        endtimeMs: 0,
            //        period: 0,
            //        numSamples: 25,
            //        tagName: "LOOP_5_PV",
            //        displayMode: displayMode,
            //        dataMode: 1,
            //        instantTrend: 0,
            //        samplePeriod: 250,
            //        cluster: "Cluster1")
            //    .ToList();

            var trends = ctApi
                .TrnQuery(
                    endtime: DateTime.Now,
                    period: 0,
                    numSamples: 25,
                    tagName: "LOOP_5_PV",
                    displayMode: displayMode,
                    dataMode: 1,
                    instantTrend: 0,
                    samplePeriod: 250,
                    cluster: "Cluster1")
                .ToList();
        }
    }
}
