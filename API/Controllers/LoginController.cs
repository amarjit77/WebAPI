using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        // GET: /Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            // Simple authentication logic for demonstration
            if (username == "admin" && password == "password")
            {
                ViewBag.Message = "Login successful!";
            }
            else
            {
                ViewBag.Message = "Invalid username or password.";
            }
            return View();
        }
    }
}
