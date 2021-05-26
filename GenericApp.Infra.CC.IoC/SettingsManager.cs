using Microsoft.Extensions.Configuration;

namespace GenericApp.Infra.CC.IoC
{
    public static class SettingsManager
    {
        public static void SetGlobalSettings(IConfiguration configuration)
        {
            Global.Instance.SetConnectionString(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
