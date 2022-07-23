using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reimbursement_system.Model;
using bcrypt = BCrypt.Net.BCrypt;

namespace Reimbursement_system.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserDBContext _context;

        public RegisterController(IConfiguration configuration, UserDBContext context)
        {
            _context = context;
            _configuration = configuration;

        }
        [HttpPost]
        //[Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (checkUser == null)
            {
                user.Password = bcrypt.HashPassword(user.Password, 12);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok("User Created Successfully");
            }
            else
            {
                return BadRequest("User already exsits");
            }
        }
    }
}
