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
            using (var ctApi = serviceProvider.GetService<CtApi>())
            {
                try
                {
                    ctApi.Open("", "", "");
                    var tag = ctApi.TagRead("Local_ToolTip");
                    var tag2 = ctApi.TagRead("Local_IsAutomatic");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
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
                options.AddFilter<DebugLoggerProvider>(null /* category*/ , LogLevel.Information /* min level */);
                options.AddFilter<ConsoleLoggerProvider>(null  /* category*/ , LogLevel.Information /* min level */);
            })
            .AddTransient<CtApi>(); // Register service from the library
        }
    }
}
