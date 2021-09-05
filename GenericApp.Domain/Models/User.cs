using GenericApp.Domain.Models.Base;
using GenericApp.Infra.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericApp.Domain.Models
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Column("name"), MaxLength(40), Required]
        public string Name { get; set; }
        [Column("email"), EmailAddress, MaxLength(30), Required]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("active"), Required]
        public bool Active { get; set; }
        [Column("role"), Required]
        public Role Role { get; set; }
        [Column("password_valid_date")]
        public DateTime? PasswordValidDate { get; set; }
    }
}
