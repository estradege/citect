using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System;
using Dapper;
using System.Linq;

namespace Citect.ConsoleApp
{
    class Program
    {
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(config =>
            {
                config.AddDebug();      // Log to debug (debug window in Visual Studio or any debugger attached)
                config.AddConsole();    // Log to console (colored !)
            })
            .Configure<LoggerFilterOptions>(options =>
            {
                options.AddFilter<DebugLoggerProvider>(null, LogLevel.Debug);
                options.AddFilter<ConsoleLoggerProvider>(null, LogLevel.Debug);
            })
            .AddTransient<CtApi>();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Citect!");

            // Setting up dependency injection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Get an instance of the service
            var ctApi = serviceProvider.GetService<CtApi>();

            //using (var db = new AlarmDbConnection("AlarmServer1", @"C:\ProgramData\Schneider Electric\Citect SCADA 2018\User\Factory Controls\Systems.xml"))
            //{
            //    var result = db.Query("SELECT * FROM CiAlarmObject");
            //    var result2 = db.Query<Alm>("SELECT FullName, Description FROM CiAlarmObject");
            //    var result3 = db.Query<Alm>("SELECT * FROM CiAlarmObject");
            //}
        }

        class Alm
        {
            public string FullName { get; set; }
            public string Description { get; set; }
        }
    }
}
