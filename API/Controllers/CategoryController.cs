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
        [Route("GetAllCategory")]
        public IActionResult GetAllCategory()
        {
            var result = repository.GetCategories();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCategoryById/{id}")]
        public IActionResult GetCategory(int id)
        {
            var result = repository.GetCategoryById(id);
            return Ok(result);
        }
    }
}
