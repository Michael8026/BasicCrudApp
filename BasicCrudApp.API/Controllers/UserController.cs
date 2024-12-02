using BasicCrudApp.Domain;
using BasicCrudApp.Domain.Common;
using BasicCrudApp.Domain.Entities;
using BasicCrudApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicCrudApp.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var result = new Result<List<User>>();
            result.RequestTime = DateTime.UtcNow;

            var response = await _service.GetUsers();
            result = response;
            result.ResponseTime = DateTime.UtcNow;
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetUser(long id)
        {
            var result = new Result<User>();
            result.RequestTime = DateTime.UtcNow;

            var response = await _service.GetUser(id);
            result = response;
            result.ResponseTime = DateTime.UtcNow;
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto user)
        {
            var result = new Result<long>();
            result.RequestTime = DateTime.UtcNow;

            var response = await _service.CreateUser(user);
            result = response;
            result.ResponseTime = DateTime.UtcNow;
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> UpdateUser(long id, [FromBody] UserDto user)
        {
            var result = new Result<bool>();
            result.RequestTime = DateTime.UtcNow;

            var response = await _service.UpdateUser(id, user);
            result = response;
            result.ResponseTime = DateTime.UtcNow;
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> RemoveUser(long id)
        {
            var result = new Result<bool>();
            result.RequestTime = DateTime.UtcNow;

            var response = await _service.RemoveUser(id);
            result = response;
            result.ResponseTime = DateTime.UtcNow;
            return Ok(result);
        }

    }
}
