using GenericApp.Domain.Interfaces.Repositories;
using GenericApp.Domain.Models;
using GenericApp.Infra.Data.Interfaces;
using GenericApp.Infra.Data.Repositories.Base;

namespace GenericApp.Infra.Data.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
