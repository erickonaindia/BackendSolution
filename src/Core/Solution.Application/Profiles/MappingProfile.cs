using AutoMapper;
using Solution.Application.Features.Categories.DTOs;
using Solution.Domain.Entities;

namespace Solution.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
        }
    }
}
