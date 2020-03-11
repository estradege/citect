using Citect;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctApi = new CtApi(true))
            {
                var trends = ctApi.TrnQuery(
                    starttime: DateTime.Now.AddHours(-1),
                    endtime: DateTime.Now,
                    period: 300,
                    tagName: "LOOP_5_PV",
                    displayMode: DisplayMode.Get(Ordering.OldestToNewest, Condense.Mean, Stretch.Step, 0, BadQuality.Zero, Raw.None),
                    dataMode: 1,
                    cluster: "Cluster1");
            }
        }
    }
}
