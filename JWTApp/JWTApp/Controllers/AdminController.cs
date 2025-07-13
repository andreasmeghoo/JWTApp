using JWTApp.Data;
using JWTApp.Models;
using JWTApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {

        private readonly JWTAppDBContext _context;

        public AdminController(JWTAppDBContext context)
        {
            _context = context;
        }

        [HttpPost("ApproveUser")]
        public async Task<IActionResult> PostApproveUser([FromBody] ApproveUserRequest request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
                return NotFound("User not found");

            if (request.Role != "Admin" && request.Role != "User")
            {
                return StatusCode(400, "Invalid Role");
            }

            if(user.IsApproved == true)
            {
                return StatusCode(400, "User is already approved");
            }

            user.IsApproved = true;
            user.Role = request.Role;

            await _context.SaveChangesAsync();

            return Ok($"User {user.Username} approved and assigned role {user.Role}");
        }

        [HttpPost("DeactivateUser")]
        public async Task<IActionResult> PostDeactivateUser([FromBody] int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("User not found");

            if(user.IsApproved == false)
            {
                return StatusCode(400, "User is already deactived or was never approved.");
            }

            user.IsApproved = false;

            await _context.SaveChangesAsync();

            return Ok($"User {user.Username}'s account has been deactivated");
        }

        [HttpGet("UnapprovedUsers")]
        public ActionResult<List<UserViewModel>>GetUnapprovedUsers()
        {
            return _context.Users.Where(x => !x.IsApproved)
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Username = x.Username,
                    Role = x.Role,
                    IsApproved = x.IsApproved
                })
                .ToList(); 
        }

        [HttpGet("ApprovedUsers")]
        public ActionResult <List<UserViewModel>>GetApprovedUsers()
        {
            return _context.Users.Where(x => x.IsApproved)
              .Select(x => new UserViewModel
              {
                  Id = x.Id,
                  Username = x.Username,
                  Role = x.Role,
                  IsApproved = x.IsApproved
              })
              .ToList();
        }

    }
}
