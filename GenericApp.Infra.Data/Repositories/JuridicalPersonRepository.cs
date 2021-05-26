using GenericApp.Domain.Interfaces.Repositories;
using GenericApp.Domain.Models;
using GenericApp.Infra.Data.Repositories.Base;

namespace GenericApp.Infra.Data.Repositories
{
    public class JuridicalPersonRepository : BaseRepository<JuridicalPerson>, IJuridicalPersonRepository
    {
        public JuridicalPersonRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
