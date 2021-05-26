namespace GenericApp.Domain.Interfaces.Services.Base
{
    public interface IBaseService
    {
        TService GetService<TService>() where TService : class;
    }
}
