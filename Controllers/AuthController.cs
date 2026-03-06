using ContestSystem.Data;
using ContestSystem.DTOs;
using ContestSystem.Enums;
using ContestSystem.Models;
using ContestSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace ContestSystem.Controllers
{
    [ApiController]
    [EnableRateLimiting("fixed")]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService;

        public AuthController(AppDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            if (_context.Users.Any(x => x.Email == dto.Email))
            {
                return BadRequest("Email already exists");
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password, // later we hash
                Role = Enum.Parse<UserRole>(dto.Role)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User Registered Successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Email == dto.Email && x.Password == dto.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _authService.GenerateToken(user);

            return Ok(new
            {
                Token = token,
                Role = user.Role
            });
        }


    }
}
