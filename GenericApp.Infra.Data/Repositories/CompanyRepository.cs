using GenericApp.Domain.Interfaces.Repositories;
using GenericApp.Domain.Models;
using GenericApp.Infra.Data.Interfaces;
using GenericApp.Infra.Data.Repositories.Base;

namespace GenericApp.Infra.Data.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
