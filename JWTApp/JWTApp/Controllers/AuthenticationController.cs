using JWTApp.Data;
using JWTApp.Models;
using JWTApp.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace JWTApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JWTAppDBContext _context;
        private readonly JWTTokenService _tokenService;

        public AuthenticationController(JWTAppDBContext context, JWTTokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Models.LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            if (!user.IsApproved)
                return Unauthorized("Account is pending approval.");

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Invalid credentials.");

            var token = _tokenService.GenerateToken(user.Id.ToString(), user.Username, user.Role);
            return Ok(new { token });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Models.RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Username and password are required.");

            if (request.Password.Length < 6)
                return BadRequest("Password must be at least 6 characters.");

            var exists = await _context.Users.AnyAsync(u => u.Username == request.Username);
            if (exists)
                return Conflict("Username already exists.");

            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Username = request.Username,
                IsApproved = false
            };
            user.PasswordHash = hasher.HashPassword(user, request.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Accepted("Registration submitted and pending approval.");
        }
    }
}
