using Citect;
using System;
using System.Globalization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctApi = new CtApi();

            try
            {
                ctApi.SetCtApiDirectory(@"C:\ProgramData\CitectCtApi");
                ctApi.Open(mode: CtOpen.Extended);
                Console.WriteLine("connected");

                //var dspMode = DisplayMode.Get(Ordering.OldestToNewest, Condense.Mean, Stretch.Raw, 0, BadQuality.Zero, Raw.None);
                //var trn = ctApi.TrnQuery(DateTime.Now.AddMinutes(-1), DateTime.Now, 1, "IO_AI_EX_0_Out", dspMode, 1, "CITECT");

                //foreach (var t in trn)
                //{
                //    Console.WriteLine($"{t.DateTime} {t.Value} {t.Quality}");
                //}

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine(e.ToString());
            }
            finally
            {
                ctApi.Close();
                Console.ReadLine();
            }
        }
    }
}
