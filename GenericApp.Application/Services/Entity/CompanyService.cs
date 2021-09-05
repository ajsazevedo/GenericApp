using GenericApp.Domain.Models;
using GenericApp.Application.Services.Base;
using System.Linq;
using GenericApp.Domain.Dto.Models;
using GenericApp.Infra.Data.Interfaces;
using GenericApp.Domain.Interfaces.Services.Entity;

namespace GenericApp.Application.Services.Entity
{
    public class CompanyService : BaseDbService<CompanyDto, Company>, ICompanyService
    {
        public CompanyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public CompanyDto FindByCnpj(string cnpj)
        {
            return ToDto(_repository.Find(x => x.Cnpj == cnpj).FirstOrDefault());
        }

        public override CompanyDto Add(CompanyDto obj)
        {
            var existingCompany = FindByCnpj(obj.Cnpj);
            if (existingCompany != null)
                return Update(existingCompany.Id, existingCompany);

            return base.Add(obj);
        }
    }
}
