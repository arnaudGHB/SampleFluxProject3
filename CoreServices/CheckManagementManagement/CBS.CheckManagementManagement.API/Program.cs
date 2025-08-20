using NLog.Web;
using CBS.CustomLog.Logger;
using CBS.CheckManagementManagement.API;
using Serilog;
using Serilog.Events;
using CBS.CustomLog.Logger.CustomLogger;
using NLog.Extensions.Logging;
using CBS.CheckManagementManagement.Domain;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>()
             .ConfigureAppConfiguration((hostingContext, config) =>
              {
                  config.SetBasePath(Directory.GetCurrentDirectory());
                  config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                  config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                  config.AddEnvironmentVariables();
                  config.AddCommandLine(args);
              });
            webBuilder.ConfigureServices((hostContext, services) =>
            {
                var startup = new Startup(hostContext.Configuration);
                services.AddControllers();
            });
        }).UseSerilog((context, config) =>
        {
            config
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File($"{context.Configuration["LoogerSetting:LoggerPath"]}\\Log_.log", rollingInterval: RollingInterval.Day);
        })

    .ConfigureLogging((hostBuilderContext, logging) =>
    {
        logging.ClearProviders();
        logging.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.TimestampFormat = "\n[dd-MMM-yyyy hh:mm:ss]\n";
        });
    });
}
