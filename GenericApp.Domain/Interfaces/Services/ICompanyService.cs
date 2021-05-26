using GenericApp.Domain.Interfaces.Services.Base;
using GenericApp.Domain.Models;

namespace GenericApp.Domain.Interfaces.Services
{
    public interface ICompanyService : IBaseDataService<Company>
    {
        Company FindByCnpj(string cnpj);
    }
}
