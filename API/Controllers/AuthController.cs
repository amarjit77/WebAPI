using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // In-memory user store for demonstration purposes
        private static readonly Dictionary<string, string> Users = new();

        // POST: api/Auth/signup
        [HttpPost("signup")]
        public IActionResult Signup([FromBody] UserDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest(new { success = false, message = "Username and password are required." });
            }

            if (Users.ContainsKey(user.Username))
            {
                return Conflict(new { success = false, message = "Username already exists." });
            }

            Users[user.Username] = user.Password;
            return Ok(new { success = true, message = "Signup successful." });
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest(new { success = false, message = "Username and password are required." });
            }

            if (Users.TryGetValue(user.Username, out var storedPassword) && storedPassword == user.Password)
            {
                return Ok(new { success = true, message = "Login successful." });
            }

            return Unauthorized(new { success = false, message = "Invalid username or password." });
        }
    }

    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
