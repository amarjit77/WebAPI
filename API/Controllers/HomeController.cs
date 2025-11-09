using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        // Addition method for two numbers
        [HttpGet("add")]
        public ActionResult<int> Add([FromQuery] int a, [FromQuery] int b)
        {
            return a + b;
        }
    }
}
