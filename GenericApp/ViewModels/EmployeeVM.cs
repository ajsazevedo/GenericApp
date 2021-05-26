using GenericApp.Domain.Enums;
using System;

namespace GenericApp.ViewModels
{
    public class EmployeeVM
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Code { get; set; }
        public DateTime Admission { get; set; }
        public string Position { get; set; }
        public CompanyVM Company { get; set; }
        public bool Active { get; set; }
    }
}
