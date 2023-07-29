using APIIntro.Service.Dtos.Categories;
using APIIntro.Core.Entities;
using APIIntro.Core.Repositories.Interfaces;
using APIIntro.Service.Responses;
using APIIntro.Service.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using APIIntro.Service.Extensions;
using Microsoft.AspNetCore.Http;

namespace APIIntro.Service.Services.Implemantations
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _evn;
        private readonly IHttpContextAccessor _http;
        public ProductService(IProductRepository repository, IMapper mapper, ICategoryRepository categoryRepository, IWebHostEnvironment evn, IHttpContextAccessor http)
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _evn = evn;
            _http = http;
        }

        public async Task<ApiResponse> CreateAsync(ProductPostDto dto)
        {
            if (!await _categoryRepository.IsExsist(x => x.Id == dto.CategoryId))
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Category not Found"
                };
            }
            Product product = _mapper.Map<Product>(dto);
            product.Image = dto.file.CreateImage(_evn.WebRootPath, "assests/images/products");
            product.ImageUrl = _http.HttpContext.Request.Scheme + "://" + _http.HttpContext.Request.Host
                + $"assests/images/products/{product.Image}";
            await _repository.AddAsync(product);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = product
            }; 
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<Product> products = await _repository.GetAllAsync(x => !x.IsDeleted,"Category");
            return new ApiResponse
            {
                StatusCode = 200,
                items = products
            };
        }
        public async  Task<ApiResponse> GetAsync(int id)
        {
            Product? product = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted, "Category");
            if (product == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse { 
              StatusCode = 200,
              items = product
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Product? product = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (product == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            product.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 204
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, ProductUpdateDto dto)
        {
            Product? update = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (update == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            update.Name = dto.Name;
            update.UpdatedAt = DateTime.UtcNow.AddHours(4);
            update.CategoryId = dto.CategoryId;
            update.Price = dto.Price;
            update.Image = dto.file == null ? update.Image: dto.file.CreateImage(_evn.WebRootPath, "assets/images/products");
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 204
            };
        }
    }
}
