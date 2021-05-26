using GenericApp.Infra.CC.IoC.Objects;

namespace GenericApp.Infra.CC.IoC
{
    public class DependencyInjectionStartup
    {
        public static StartupParameters SetParameters()
        {
            return new StartupParameters
            {
                ConnectionString = Global.Instance.GetConnectionString()
            };
        }
    }
}
