using AutoMapper;
using DataAccess.DTO.Product;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositories;
using Repository.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductReository repository = new ProductRepository();
        private IMapper _mapper;
        public ProductController(IMapper mapper)
        {         
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllProduct")]
        public IActionResult GetAllProduct()
        {
            var result = repository.GetProducts();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var result = repository.GetProductById(id);
            return Ok(_mapper.Map<ProductDTO>(result));
        }

        [HttpGet]
        [Route("GetProductByName/{name}")]
        public IActionResult GetProductByName(string name)
        {
            var result = repository.GetProductByName(name);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetProductByPrice/{price}")]
        public IActionResult GetProductByPrice(decimal price)
        {
            var result = repository.GetProductByPrice(price);
            return Ok(_mapper.Map<ProductDTO>(result));
        }

        [HttpPost]
        [Route("AddNewProduct")]
        public IActionResult AddNewProduct([FromBody] CreateUpdateProductDTO product)
        {
             repository.SaveProduct(_mapper.Map<Product>(product));
            return Ok("Add Successfull");    
        }

        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public IActionResult UpdateProduct(int id , [FromBody]CreateUpdateProductDTO product)
        {
            var map = _mapper.Map<Product>(product);
            repository.UpdateProduct(id , map);
            return Ok("Update Successfull");
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            repository.DeleteProduct(id);
            return Ok("Delete Successfull !!!");
        }
    }
}
