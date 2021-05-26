using System.Collections.Generic;

namespace GenericApp.ViewModels
{
    public class CompanyVM
    {
        public bool Active { get; set; }
        public List<EmployeeVM> Employees { get; set; }
        public string Name { get; set; }
        public string PublicName { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long Id { get; set; }
    }
}
