using GenericApp.Domain.Models;
using GenericApp.Application.Services.Base;
using System.Linq;
using GenericApp.Domain.Dto.Models;
using GenericApp.Infra.Data.Interfaces;
using GenericApp.Domain.Interfaces.Services.Entity;

namespace GenericApp.Application.Services.Entity
{
    public class EmployeeService : BaseDbService<EmployeeDto, Employee>, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override EmployeeDto Add(EmployeeDto obj)
        {
            CheckExistingCompany(ref obj);

            var existingEmployee = FindByCpf(obj.Cpf);
            if (existingEmployee != null)
                return Update(existingEmployee.Id, existingEmployee);

            return base.Add(obj);
        }

        void CheckExistingCompany(ref EmployeeDto employee)
        {
            if(employee.Company != null)
            {
                var existingCompany = _unitOfWork.GetService<ICompanyService>().FindByCnpj(employee.Company.Cnpj);
                if (existingCompany != null)
                    employee.Company = existingCompany;
            }
        }

        public EmployeeDto FindByCpf(string cpf)
        {
            return ToDto(_repository.Find(x => x.Cpf == cpf).FirstOrDefault());
        }
    }
}
