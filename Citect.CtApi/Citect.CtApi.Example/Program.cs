using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Serilog;
using System;

namespace Citect.CtApi.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Citect!");

            // Setting up dependency injection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Get an instance of the service
            var ctApi = serviceProvider.GetService<CtApi>();
            try
            {
                ctApi.Open("", "", "");
                var tooltip = ctApi.TagRead("Local_ToolTip");
                var isAuto = ctApi.TagRead("Local_IsAutomatic");
                ctApi.TagWrite("Local_ToolTip", DateTime.Now.ToString());
                ctApi.TagWrite("Local_IsAutomatic", isAuto == "0" ? "1" : "0");

                var result = ctApi.Cicode("PageDisplay(Alarm)");
                Console.WriteLine($"PageDisplay(Alarm) = {result}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                ctApi.Close();
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.AddDebug(); // Log to debug (debug window in Visual Studio or any debugger attached)
                config.AddConsole(); // Log to console (colored !)
            })
            .Configure<LoggerFilterOptions>(options =>
            {
                options.AddFilter<DebugLoggerProvider>(null /* category*/ , LogLevel.Debug /* min level */);
                options.AddFilter<ConsoleLoggerProvider>(null  /* category*/ , LogLevel.Debug /* min level */);
            })
            .AddTransient<CtApi>(); // Register service from the library
        }
    }
}
