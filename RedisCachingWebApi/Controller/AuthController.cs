using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedisCachingWebApi.Application.Models.LoginJWT;
using RedisCachingWebApi.Data;
using RedisCachingWebApi.Services;

namespace RedisCachingWebApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // This controller can be used for authentication-related endpoints.
        // For example, you can implement login, logout, and token generation here.

        private readonly AppDbContext _appDbContext;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext appDbContext, TokenService tokenService)
        {
            _appDbContext = appDbContext;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginModel loginModel)
        {
            if (await _appDbContext.Users.AnyAsync(u => u.Username == loginModel.Username))
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = loginModel.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(loginModel.Password),
                Role =  "Employee" // Default role, can be changed as needed
            };

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }
            var token = _tokenService.GenerateToken(user.Username, user.Role);
            return Ok(new { Token = token });
        }
    }
}