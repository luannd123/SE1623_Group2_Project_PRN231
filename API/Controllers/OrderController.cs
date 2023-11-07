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
        [Route("GetOrders")]
        public IActionResult GetOrders()
        {
            var result = repository.GetOrders();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetOrdersByDate/{From}/{To}")]
        public IActionResult GetOrderByDate(DateTime From , DateTime To)
        {
            var result = repository.GetOrderByDate(From, To);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddNewOrder")]
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
        [HttpPut]
        [Route("UpdateOrder/{id}")]
        public IActionResult UpdateOrder(int id , [FromBody] CreateUpdateOrderDTO order)
        {
            try
            {
                var map = _mapper.Map<Order>(order);
                repository.UpdateOrder(id , map);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Update Successfull !!!");
        }

        [HttpDelete]
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
