using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GenericApp.Domain.Models
{
    [Table("Customer")]
    public class Customer : Person
    {
        public List<Order> Orders { get; set; }
    }
}
