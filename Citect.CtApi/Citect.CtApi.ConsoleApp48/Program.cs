using System;
using System.Runtime.InteropServices;

namespace ConsoleApp48
{
    internal class Program
    {
        [DllImport("CtApi.dll", EntryPoint = "ctOpen", SetLastError = true)]
        private static extern IntPtr CtOpen(string sComputer, string sUser, string sPassword, uint nMode);

        [DllImport("CtApi.dll", EntryPoint = "ctClose", SetLastError = true)]
        private static extern bool CtClose(IntPtr hCTAPI);

        [DllImport("CtApi.dll", EntryPoint = "ctSetManagedBinDirectory", SetLastError = true)]
        private static extern bool CtSetManagedBinDirectory(string sPath);

        static void Main(string[] args)
        {
            var ctApi = IntPtr.Zero;

            try
            {
                var result = CtSetManagedBinDirectory("C:\\ProgramData\\CitectCtApi");
                ctApi = CtOpen(null, null, null, 0);
                Console.WriteLine("Opened");
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
                var result = CtClose(ctApi);
                Console.WriteLine("Closed");
            }
        }
    }
}
