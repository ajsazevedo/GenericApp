using GenericApp.Application.Interfaces.Repositories.Base;
using GenericApp.Domain.Models;

namespace GenericApp.Application.Interfaces.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
    }
}
