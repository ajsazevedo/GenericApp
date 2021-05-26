using GenericApp.Domain.Interfaces.Services.Base;

namespace GenericApp.Services.Base
{
    public class BaseService : IBaseService
    {
        public TService GetService<TService>() where TService : class
        {
            throw new System.NotImplementedException();
        }
    }
}
