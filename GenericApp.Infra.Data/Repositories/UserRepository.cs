using GenericApp.Domain.Interfaces.Repositories;
using GenericApp.Domain.Models;
using GenericApp.Infra.Data.Interfaces;
using GenericApp.Infra.Data.Repositories.Base;

namespace GenericApp.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
