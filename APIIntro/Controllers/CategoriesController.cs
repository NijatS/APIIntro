using APIIntro.Context;
using APIIntro.Dtos.Categories;
using APIIntro.Entities;
using APIIntro.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIntro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public CategoriesController(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Category> categories = await _repository.GetAllAsync(x => !x.IsDeleted);
            return StatusCode(200,categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Category? category = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if(category == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200,category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryPostDto dto)
        {
            if (await _repository.IsExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower())) {
                return StatusCode(400, new { description = $"{dto.Name} Already exists" });
            }
            Category category = _mapper.Map<Category>(dto); 
            await _repository.AddAsync(category);
            await _repository.SaveAsync();
            return StatusCode(201, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Category? category = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (category == null)
            {
                return StatusCode(404);
            }
            category.IsDeleted = true;
            await _repository.SaveAsync();
            return StatusCode(204);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto dto)
        {
            if (await _repository.IsExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return StatusCode(400, new { description = $"{dto.Name} Already exists" });
            }

            Category? update = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (update == null)
            {
                return StatusCode(404);
            }
            update.Name = dto.Name;
            update.Description = dto.Description;
            await _repository.SaveAsync();
            return StatusCode(204);
        }
    }
}
