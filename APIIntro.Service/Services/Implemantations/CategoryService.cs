using APIIntro.Service.Dtos.Categories;
using APIIntro.Core.Entities;
using APIIntro.Core.Repositories.Interfaces;
using APIIntro.Service.Responses;
using APIIntro.Service.Services.Interfaces;
using AutoMapper;

namespace APIIntro.Service.Services.Implemantations
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(CategoryPostDto dto)
        {
            if (await _repository.IsExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Category category = _mapper.Map<Category>(dto);
            await _repository.AddAsync(category);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = category
            }; 
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<Category> categories = await _repository.GetAllAsync(x => !x.IsDeleted);
            return new ApiResponse
            {
                StatusCode = 200,
                items = categories
            };
        }
        public async  Task<ApiResponse> GetAsync(int id)
        {
            Category? category = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (category == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            return new ApiResponse { 
              StatusCode = 200,
              items = category
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Category? category = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (category == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            category.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 204
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            if (await _repository.IsExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }

            Category? update = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (update == null)
            {
                return new ApiResponse
                {
                    StatusCode = 404
                };
            }
            update.Name = dto.Name;
            update.Description = dto.Description;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 204
            };
        }
    }
}
