using Erp.Auth.Services;
using ERP.Business.Services.Interfaces;
using ERP.Infrastructure.Entities;
using ERP.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        public AuthController(ITokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
           
            var user = await _userService.GetUserByEmail(request.Email);
                

            if (user == null)
                return Unauthorized("Invalid credentials");

            
            if (user.Password != request.Password)
                return Unauthorized("Invalid credentials");

            
            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
        [HttpGet("secure")]
        [Authorize(Roles = "Admin")]
        public IActionResult SecureEndpoint()
        {
            return Ok("This is protected data visible only to Admins.");
        }
    }
}
