using GenericApp.Infra.CC.IoC.Objects;

namespace GenericApp.Infra.CC.IoC
{
    public static class DependencyInjectionStartup
    {
        public static StartupParameters SetParameters()
        {
            return new StartupParameters
            {
                ConnectionString = Global.Instance.GetConnectionString(),
                EmailConfiguration = Global.Instance.GetEmailConfiguration(),
                TokenConfiguration = Global.Instance.GetTokenConfiguration(),
                GeneralSettings = Global.Instance.GetConfigurationFile()
            };
        }
    }
}
