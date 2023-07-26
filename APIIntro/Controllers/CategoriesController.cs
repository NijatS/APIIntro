using APIIntro.Context;
using APIIntro.Dtos.Categories;
using APIIntro.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIntro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public CategoriesController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Category> categories = await _context.Categories.Where(x=>!x.IsDeleted)
                .ToListAsync();
            return StatusCode(200,categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Category? category =await  _context.Categories
                .Where(x => x.Id == id && !x.IsDeleted)
                .FirstOrDefaultAsync();
            if(category == null)
            {
                return StatusCode(404);
            }
            return StatusCode(200,category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryPostDto dto)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}
            if(_context.Categories.Any(x=>x.Name.Trim().ToLower() == dto.Name.Trim().ToLower())) {
                return StatusCode(400, new { description = $"{dto.Name} Already exists" });
            }
            Category category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(201, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Category? category = await _context.Categories
              .Where(x => x.Id == id && !x.IsDeleted)
              .FirstOrDefaultAsync();
            if (category == null)
            {
                return StatusCode(404);
            }
            category.IsDeleted = true;
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (_context.Categories.Any(x => x.Name.Trim().ToLower() == category.Name.Trim().ToLower()))
            {
                return StatusCode(400, new { description = $"{category.Name} Already exists" });
            }

            Category? update = await _context.Categories
              .Where(x => x.Id == id && !x.IsDeleted)
              .FirstOrDefaultAsync();
            if (update == null)
            {
                return StatusCode(404);
            }
            update.Name = category.Name;
            update.Description = category.Description;
            await _context.SaveChangesAsync();
            return StatusCode(204);
        }
    }
}
