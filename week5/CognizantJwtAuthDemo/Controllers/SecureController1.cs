using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecureController : ControllerBase
    {
        [Authorize]
        [HttpGet("data")]
        public IActionResult GetSecureData()
        {
            var username = User.Identity.Name;
            return Ok(new { Message = $"Hello {username}, this is a secure message." });
        }
    }
}
