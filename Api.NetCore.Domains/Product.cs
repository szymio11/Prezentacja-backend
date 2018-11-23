using System;

namespace Api.NetCore.Domains
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductType Type { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}