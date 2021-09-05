using Microsoft.Extensions.Configuration;
using Serilog;

namespace GenericApp.Infra.CC.Logging.Serilog
{
    public static class LoggerManager
    {
        public static void ConfigureSerilog(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public static void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}
