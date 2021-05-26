using GenericApp.Domain.Interfaces.Services;
using GenericApp.Domain.Models;
using GenericApp.Infra.Data;
using GenericApp.Services.Base;
using System.Linq;

namespace GenericApp.Services
{
    public class CompanyService : BaseDbService<Company>, ICompanyService
    {
        public CompanyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Company FindByCnpj(string cnpj)
        {
            return _repository.Find(x => x.Cnpj == cnpj).FirstOrDefault();
        }

        public override Company Add(Company obj)
        {
            var existingCompany = FindByCnpj(obj.Cnpj);
            if (existingCompany != null)
                return Update(existingCompany);

            return base.Add(obj);
        }
    }
}
