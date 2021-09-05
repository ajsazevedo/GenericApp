using GenericApp.Domain.Dto.Models;
using GenericApp.Domain.Interfaces.Services.Base;

namespace GenericApp.Domain.Interfaces.Services.Entity
{
    public interface IEmployeeService : IBaseDbService<EmployeeDto>
    {
        EmployeeDto FindByCpf(string cpf);
    }
}
