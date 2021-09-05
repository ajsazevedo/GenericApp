using GenericApp.Infra.Common.Enums;
using GenericApp.Domain.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Domain.Models
{
    public abstract class Person : BaseEntity
    {
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string LastName { get; set; }
        [MaxLength(11)]
        public string Cpf { get; set; }
        [EmailAddress]
        [MaxLength(30)]
        public string Email { get; set; }
        [MaxLength(15)]
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}