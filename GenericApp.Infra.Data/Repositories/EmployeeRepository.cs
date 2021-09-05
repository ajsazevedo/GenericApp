using GenericApp.Domain.Interfaces.Repositories;
using GenericApp.Domain.Models;
using GenericApp.Infra.Data.Interfaces;
using GenericApp.Infra.Data.Repositories.Base;

namespace GenericApp.Infra.Data.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
