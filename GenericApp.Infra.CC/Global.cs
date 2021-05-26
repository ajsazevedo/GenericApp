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
    }
}
