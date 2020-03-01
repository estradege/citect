using Citect.CtApi;
using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctApi = new CtApi())
            {
                ctApi.Open();
                ctApi.TagWrite("Local_Tooltip", DateTime.Now.ToLongTimeString());
                var myTag = ctApi.TagRead("Local_Tooltip");

                Console.WriteLine(myTag);
            }
        }
    }
}
