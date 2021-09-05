using GenericApp.Domain.Models.Base;
using GenericApp.Infra.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericApp.Domain.Models
{
    public class User : BaseEntity
    {
        [MaxLength(40)]
        public string Name { get; set; }
        [EmailAddress]
        [MaxLength(30)]
        public string Email { get; set; }
        [MaxLength(12)]
        public string Password { get; set; }
        [Column("active"), Required]
        public bool Active { get; set; }
        [Column("role"), Required]
        public Role Role { get; set; }
        [Column("password_valid_date")]
        public DateTime? PasswordValidDate { get; set; }
    }
}
