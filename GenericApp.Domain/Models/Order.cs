using GenericApp.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericApp.Domain.Models
{
    public class Order : BaseEntity<long>
    {
        public Customer Customer { get; set; }
        public decimal Value { get; set; }
    }
}
