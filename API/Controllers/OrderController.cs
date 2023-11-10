using AutoMapper;
using DataAccess.DTO.Order;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositories;
using Repository.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository repository = new OrderRepository();
        private IMapper _mapper;
        public OrderController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var result = repository.GetOrders();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetOrdeDetailByOrderId/{id}")]
        public IActionResult GetOrderDetail(int id)
        {
            var result = repository.GetOrderDetail(id);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetOrderById/{id}")]
        public IActionResult GetOrder(int id)
        {
            var result = repository.GetOrder(id);
            return Ok(result);
        }
        [HttpGet("{From}/{To}")]
        public IActionResult GetOrderByDate(DateTime From , DateTime To)
        {
            var result = repository.GetOrderByDate(From, To);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddNewOrder(CreateUpdateOrderDTO order)
        {
            try
            {
                repository.SaveOrder(_mapper.Map<Order>(order));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            return Ok("Add Successfull !!!");    
        }
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id , [FromBody] CreateUpdateOrderDTO order)
        {
            try
            {
                var map = _mapper.Map<Order>(order);
                repository.UpdateOrder(map);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Update Successfull !!!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                repository.DeleteOrder(id);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Delete Sucessfull !!!");
        }
    }
}
