using GenericApp.Domain.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace GenericApp.Domain.Models
{
    public abstract class JuridicalPerson : BaseEntity
    {
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(40)]
        public string PublicName { get; set; }
        [MaxLength(15)]
        public string Cnpj { get; set; }
        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(15)]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}