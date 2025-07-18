using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            var username = User.Identity?.Name;
            return Ok(new
            {
                Message = $"Welcome, {username}!",
                Role = "Admin"
            });
        }
    }
}
       