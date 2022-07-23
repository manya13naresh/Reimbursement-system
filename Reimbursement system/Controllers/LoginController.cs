using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Reimbursement_system.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using bcrypt = BCrypt.Net.BCrypt;

namespace Reimbursement_system.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserDBContext _context;

        public LoginController(IConfiguration configuration, UserDBContext context)
        {
            _context = context;
            _configuration = configuration;

        }
        [HttpPost]
//[Route("login")]
        public async Task<ActionResult<User>> Login([FromBody]Login user)
        {
            var checkemail = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (checkemail!= null)
            {
                if (bcrypt.Verify(user.Password, checkemail.Password))
                {
                    var token = CreateToken(checkemail);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
            }
            else
            {
                return BadRequest("User not found");
            }
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
