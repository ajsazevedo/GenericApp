using Microsoft.Extensions.Configuration;

namespace GenericApp.Infra.CC.IoC.Objects
{
    public class StartupParameters
    {
        public string ConnectionString { get; set; }
        public IConfigurationSection EmailConfiguration { get; set; }
        public IConfigurationSection TokenConfiguration { get; set; }
        public IConfiguration GeneralSettings { get; set; }
    }
}
