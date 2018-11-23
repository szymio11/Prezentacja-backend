using Api.NetCore.ModelsDto.Products;
using AutoMapper;

namespace Api.NetCore.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Domains.Product>()
                .ForMember(a => a.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}