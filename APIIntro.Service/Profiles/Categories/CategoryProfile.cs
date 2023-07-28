using APIIntro.Service.Dtos.Categories;
using APIIntro.Core.Entities;
using AutoMapper;

namespace APIIntro.Service.Profiles.Categories
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryPostDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
