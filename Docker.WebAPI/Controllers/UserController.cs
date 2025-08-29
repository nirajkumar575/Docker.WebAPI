using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Docker.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // Sample in-memory user list
        private static readonly List<string> Users = new List<string>
        {
            "Alice",
            "Bob",
            "Charlie"
        };

        // GET: api/user
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetUsers()
        {
            return Ok(Users);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<string> GetUser(int id)
        {
            if (id < 0 || id >= Users.Count)
                return NotFound();

            return Ok(Users[id]);
        }

        // POST: api/user
        [HttpPost]
        public ActionResult AddUser([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name is required.");

            Users.Add(name);
            return CreatedAtAction(nameof(GetUser), new { id = Users.Count - 1 }, name);
        }
    }
}