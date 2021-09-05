using Microsoft.Extensions.Configuration;

namespace GenericApp.Infra.CC.IoC
{
    public static class SettingsManager
    {
        public static void SetGlobalSettings(IConfiguration configuration)
        {
            Global.Instance.SetConnectionString(configuration.GetConnectionString("DefaultConnection"));
            Global.Instance.SetConfigurationFile(configuration);
            Global.Instance.SetTokenConfiguration(configuration.GetSection("TokenConfigurations"));
            Global.Instance.SetEmailConfiguration(configuration.GetSection("EmailSettings"));
        }
    }
}
