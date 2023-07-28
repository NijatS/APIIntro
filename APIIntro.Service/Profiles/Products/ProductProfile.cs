using APIIntro.Service.Dtos.Categories;
using APIIntro.Core.Entities;
using AutoMapper;
using APIIntro.Service.Dtos.Products;

namespace APIIntro.Service.Profiles.Categories
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductPostDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
