using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericApp.Domain.Models
{
    [Table("Employee")]
    public class Employee : Person
    {
        [MaxLength(8)]
        public string Code { get; set; }
        public DateTime Admission { get; set; }
        public string Position { get; set; }
        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public bool Active { get; set; }
    }
}