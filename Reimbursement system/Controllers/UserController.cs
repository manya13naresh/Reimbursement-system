using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reimbursement_system.Model;

namespace Reimbursement_system.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserDBContext _context;

        public UserController(IConfiguration configuration, UserDBContext context)
        {
            _context = context;
            _configuration = configuration;

        }
        [HttpPost]
        [Route("billdetails"), Authorize(Roles = "User")]
        public async Task<IActionResult> BillDetails(BillDetail bill)
        {
            _context.BillDetails.Add(bill);
            await _context.SaveChangesAsync();
            return Ok("STATUS:Pending");
        }
    }
}
