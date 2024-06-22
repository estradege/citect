using Citect;
using System;

namespace ConsoleApp48
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var ctApi = new CtApi();
                ctApi.SetCtApiDirectory(@"C:\Program Files (x86)\AVEVA Plant SCADA\Bin\Bin (x64)");
                ctApi.Open("127.0.0.1", "engineer", "Citect");

                Console.WriteLine("connected");
                ctApi.Close();
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
