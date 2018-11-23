using System;
using Api.NetCore.Domains;
using Api.NetCore.ModelsDto.Categories;
using AutoMapper;
using Logic.Interfaces;

namespace Api.NetCore.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Category>()
                .ForMember(a=>a.Id,opt=>opt.Ignore())
                .ReverseMap();
            CreateMap<Guid, Category>().ConvertUsing<GuidToCategoryConverter>();
        
        }
        public class GuidToCategoryConverter : ITypeConverter<Guid, Category>
        {
            private readonly ICategoryLogic _categoryLogic;

            public GuidToCategoryConverter(ICategoryLogic categoryLogic)
            {
                _categoryLogic = categoryLogic;
            }

            public Category Convert(Guid source, Category destination, ResolutionContext context)
            {
                var categoryResult = _categoryLogic.GetAsync(source).Result;

                if (categoryResult.Success)
                {
                    return categoryResult.Value;
                }

                return null;
            }
        }

     
    }
}