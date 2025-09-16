using ERP.Business.Services.Interfaces;
using ERP.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("User data is required.");
            await _userService.RegisterUser(user);
            // Return 201 Created + location header
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        // PUT: api/users/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
        {
            if (id != user.Id)
                return BadRequest("User ID mismatch.");
            var result = await _userService.UpdateUser(user);
            if (result == 0)
                return NotFound();
            return NoContent();
        }
        // DELETE: api/users/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
                return NotFound();
            await _userService.DeleteUser(user);
            return NoContent();
        }

    }
}
