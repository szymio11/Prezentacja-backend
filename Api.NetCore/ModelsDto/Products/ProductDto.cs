using System;
using Api.NetCore.Domains;

namespace Api.NetCore.ModelsDto.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductType Type { get; set; }
        public Guid Category { get; set; }
    }
}