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
    public class AdminController
    {

        private readonly JWTAppDBContext _context;

        public AdminController(JWTAppDBContext context)
        {
            _context = context;
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
