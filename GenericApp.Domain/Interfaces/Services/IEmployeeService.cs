using GenericApp.Domain.Interfaces.Services.Base;
using GenericApp.Domain.Models;

namespace GenericApp.Domain.Interfaces.Services
{
    public interface IEmployeeService : IBaseDataService<Employee>
    {
        Employee FindByCpf(string cpf);
    }
}
