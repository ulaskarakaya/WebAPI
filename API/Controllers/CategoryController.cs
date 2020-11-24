using System.Linq;
using API.DAL.Context;
using API.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using var context = new ProjectContext();
            return Ok(context.Categories.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using var context = new ProjectContext();
            var category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            using var context = new ProjectContext();
            var updatedCategory = context.Find<Category>(category.Id);
            if (updatedCategory == null)
                return NotFound();
            updatedCategory.Name = category.Name;
            context.Update(updatedCategory);
            context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using var context = new ProjectContext();
            var deletedCategory = context.Categories.Find(id);
            if (deletedCategory == null)
                return NotFound();
            context.Remove(deletedCategory);
            context.SaveChanges();
            return NoContent();
        }


        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            using var context = new ProjectContext();

            context.Categories.Add(category);
            context.SaveChanges();
            return Created("", category);
        }

        [HttpGet("{id}/blogs")]
        public IActionResult GetWithBlogsById(int id)
        {
            using var context = new ProjectContext();
            var category = context.Categories.Find(id);
            if (category == null)
                return NotFound();
            var categoryWithBlogs = context.Categories.Where(I => I.Id == id).Include(I => I.Blogs).ToList();
            return Ok(categoryWithBlogs);
        }
    }
}
