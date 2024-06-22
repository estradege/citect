using Citect;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctApi = new CtApi())
            {
                try
                {
                    ctApi.SetCtApiDirectory(@"C:\Program Files (x86)\AVEVA Plant SCADA\Bin\Bin (x64)");
                    ctApi.Open();
                    //var myTag = ctApi.TagRead("E_ANAMOTVALVE_0_AUTHMODEii");
                    var myTag2 = ctApi.TagReadEx("E_ANAMOTVALVE_0_AUTHMODE");
                }
                catch (Exception e)
                {
                }
                finally
                {
                    ctApi.Close(); 
                }
            }
        }
    }
}
