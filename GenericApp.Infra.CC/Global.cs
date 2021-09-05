using Microsoft.Extensions.Configuration;

namespace GenericApp.Infra.CC
{
    public class Global
    {
        private static volatile Global i_instance = null;
        private static readonly Global _global = new Global();

        public static Global Instance
        {
            get
            {
                if (i_instance == null)
                {
                    lock (_global)
                    {
                        if (i_instance == null)
                        {
                            i_instance = new Global();
                        }
                    }
                }
                return i_instance;
            }
        }

        private string ConnectionString;
        public void SetConnectionString(string str)
        {
            ConnectionString = str;
        }
        public string GetConnectionString()
        {
            return ConnectionString;
        }

        private IConfigurationSection EmailConfiguration;

        public void SetEmailConfiguration(IConfigurationSection value)
        {
            EmailConfiguration = value;
        }

        public IConfigurationSection GetEmailConfiguration()
        {
            return EmailConfiguration;
        }

        private IConfigurationSection TokenConfiguration;

        public void SetTokenConfiguration(IConfigurationSection value)
        {
            TokenConfiguration = value;
        }

        public IConfigurationSection GetTokenConfiguration()
        {
            return TokenConfiguration;
        }

        private IConfiguration GeneralSettings;

        public void SetConfigurationFile(IConfiguration value)
        {
            GeneralSettings = value;
        }

        public IConfiguration GetConfigurationFile()
        {
            return GeneralSettings;
        }
    }
}
