using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reimbursement_system.Model;

namespace Reimbursement_system.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserDBContext _context;

        public AdminController(IConfiguration configuration, UserDBContext context)
        {
            _context = context;
            _configuration = configuration;

        }
        [HttpGet]
        [Route("getusers"), Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetUsers()
        {
            var users = _context.Users.Select(x => new { Id = x.Id, UserName = x.UserName }).ToList();

            return Ok(users);
        }

    }
}
