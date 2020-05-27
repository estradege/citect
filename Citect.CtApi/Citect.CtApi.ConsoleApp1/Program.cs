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
                var result = ctApi.Shutdown("", "" , 2);
                Console.WriteLine(result);
            }
        }
    }
}
