using GenericApp.Domain.Dto.Models.Base;
using System.Collections.Generic;

namespace GenericApp.Domain.Dto.Models
{
    public class CompanyDto : EntityDto
    {
        public bool Active { get; set; }
        public List<EmployeeDto> Employees { get; set; }
        public string Name { get; set; }
        public string PublicName { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
