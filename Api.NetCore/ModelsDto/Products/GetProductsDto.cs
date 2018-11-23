using System;
using Api.NetCore.Domains;
using Api.NetCore.ModelsDto.Categories;

namespace Api.NetCore.ModelsDto.Products
{
    public class GetProductsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductType Type { get; set; }
        public CategoryDto Category { get; set; }
    }
}
