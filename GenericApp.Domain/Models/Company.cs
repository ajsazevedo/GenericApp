using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GenericApp.Domain.Models
{
    [Table("Company")]
    public class Company : JuridicalPerson
    {
        public bool Active { get; set; }
        public virtual List<Employee> Employees { get; set; }
    }
}