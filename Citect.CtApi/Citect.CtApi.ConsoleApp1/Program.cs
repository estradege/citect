using Citect;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var ctApi = new CtApi();
                ctApi.SetCtApiDirectory(@"C:\Program Files (x86)\AVEVA Plant SCADA\Bin\Bin (x64)");
                ctApi.Open();
                var t = ctApi.TagRead("E_ANAMOTVALVE_0_Cmd");
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

            using (var ctApi = new CtApi())
            {
                ctApi.SetCtApiDirectory(@"C:\Program Files (x86)\AVEVA Plant SCADA\Bin\Bin (x64)");
                ctApi.Open();
                ctApi.TagWrite("MyTagName", "MyTagValueAsString");
                var myTag = ctApi.TagRead("MyTagName");
            }
        }
    }
}
