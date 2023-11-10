using AutoMapper;
using DataAccess.DTO.OrderDetail;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Repository.IRepositories;
using Repository.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private IOrderDetailRepository repository = new OrderDetailRepository();
        private IMapper _mapper;
        public OrderDetailController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrderDetails()
        {
            var result = repository.GetOrderDetails();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetailById(int id)
        {
            var result = repository.GetOrderById(id);
            return Ok(_mapper.Map<OrderDetailDTO>(result));
        }

        [HttpPost]
        public IActionResult AddNewOrderDetail(CreateUpdateOrderDetailDTO order)
        {
            try
            {
                repository.SaveOrderDetail(_mapper.Map<OrderDetail>(order));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Add Successfull !!!");
        }
        [HttpPut("{id}")]
        public IActionResult UpdateOrderDetail(int id , [FromBody]CreateUpdateOrderDetailDTO order)
        {
            try{
                var map = _mapper.Map<OrderDetail>(order);
                repository.UpdateOrderDetail(id , map);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Update Successfull !!!");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderdetail(int id) 
        {
            try
            {
                repository.DeleteOrderDetail(id);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Delete Successfull !!!");
        }
    }
}
