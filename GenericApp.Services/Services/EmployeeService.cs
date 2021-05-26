using GenericApp.Domain.Interfaces.Services;
using GenericApp.Domain.Models;
using GenericApp.Infra.Data;
using GenericApp.Services.Base;
using System.Linq;

namespace GenericApp.Services
{
    public class EmployeeService : BaseDbService<Employee>, IEmployeeService
    {
        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override Employee Add(Employee obj)
        {
            CheckExistingCompany(ref obj);

            var existingEmployee = FindByCpf(obj.Cpf);
            if (existingEmployee != null)
                return Update(existingEmployee);

            return base.Add(obj);
        }

        void CheckExistingCompany(ref Employee employee)
        {
            var existingCompany = _unitOfWork.GetService<ICompanyService>().FindByCnpj(employee.Company.Cnpj);
            if (existingCompany != null)
                employee.Company = existingCompany;
        }

        public Employee FindByCpf(string cpf)
        {
            return _repository.Find(x => x.Cpf == cpf).FirstOrDefault();
        }
    }
}
