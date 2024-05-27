using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesControllers : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok("Xin chao");
        }
    }
}
