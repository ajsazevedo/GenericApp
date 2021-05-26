using System;
using Serilog;

namespace GenericApp.Infra.CC.Exceptions.Extensions
{
    public static class ExceptionExtensions
    {
        public static void LogException(this Exception ex)
        {
            switch (ex.GetType().Name)
            {
                default:
                    Log.Error(ex, ex.Message);
                    break;
            }
        }
    }
}
