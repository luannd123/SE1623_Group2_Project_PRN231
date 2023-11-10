using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositories;
using Repository.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository repository = new CategoryRepository();     

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var result = repository.GetCategories();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var result = repository.GetCategoryById(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetProductByCategoryId/{id}")]
        public IActionResult GetProductByCategoryId(int id)
        {
            var result = repository.GetProductsByCategoryId(id);
            return Ok(result);
        }
    }
}
