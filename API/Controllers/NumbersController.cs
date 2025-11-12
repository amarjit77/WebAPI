using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumbersController : ControllerBase
    {
        // Returns a list of numbers from 1 to 100
        [HttpGet("list")]
        public ActionResult<List<int>> GetNumbers()
        {
            var numbers = new List<int>();
            for (int i = 1; i <= 100; i++)
            {
                numbers.Add(i);
            }
            return numbers;
        }
    }
}
