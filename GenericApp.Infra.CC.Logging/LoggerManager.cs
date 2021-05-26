using Serilog;

namespace GenericApp.Infra.CC.Logging.Serilog
{
    public static class LoggerManager
    {
        public static void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                //Caso seja em arquivo
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
