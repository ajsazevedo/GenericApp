using GenericApp.Domain.Models.Base;

namespace GenericApp.Domain.Models
{
    public class Order : BaseEntity
    {
        public Customer Customer { get; set; }
        public decimal Value { get; set; }
    }
}
