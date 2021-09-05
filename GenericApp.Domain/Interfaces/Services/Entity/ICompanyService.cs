using GenericApp.Domain.Dto.Models;
using GenericApp.Domain.Interfaces.Services.Base;

namespace GenericApp.Domain.Interfaces.Services.Entity
{
    public interface ICompanyService : IBaseDbService<CompanyDto>
    {
        CompanyDto FindByCnpj(string cnpj);
    }
}
