using System;

namespace Citect.CtApi.Samples.ReadWrite
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
                    ctApi.TagWrite("MyTagName", "MyTagValue");
                    var myTag = ctApi.TagRead("MyTagName");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
