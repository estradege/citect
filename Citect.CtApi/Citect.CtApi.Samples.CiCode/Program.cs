using System;

namespace Citect.CtApi.Samples.CiCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Citect!");
            try
            {
                using (var ctApi = new CtApi())
                {
                    ctApi.Open();
                    var result = ctApi.Cicode("PageDisplay(Alarm)");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
