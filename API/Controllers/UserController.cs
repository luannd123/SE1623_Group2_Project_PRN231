using AutoMapper;
using DataAccess.DTO.User;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositories;
using Repository.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository repository = new UserRepository();
        private IMapper _mapper;
        public UserController(IMapper mapper) 
        { 
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetAllUser()
        {
            var result = repository.GetUsers();
            return Ok(result);
        }
        [HttpGet]
        [Route("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var result = repository.GetUserById(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetUserByName/{name}")]
        public IActionResult GetuserByName(string name)
        {
            var result = repository.GetUserByName(name);
            return Ok(result);
        }
        [HttpPost("email,password")]
        public IActionResult GetUserByEmailAndPassword(string email , string password)
        {
            var result = repository.GetUserByEmailAndPassword(email, password);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddNewUser")]
        public IActionResult AddNewUser(CreateUpdateUserDTO user)
        {
            repository.SaveUser(_mapper.Map<User>(user));
            return Ok("Add Successfull !!!");
        }

        [HttpPut]
        [Route("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id , [FromBody] CreateUpdateUserDTO user)
        {
            var map = _mapper.Map<User>(user);
            repository.UpdateUser(id , map);
            return Ok("Update Successfull !!!");
        }
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            repository.DeleteUser(id);
            return Ok("Delete Successfull !!!!");

        }
    }
}
