namespace Docker.WebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("csv")]
        public async Task<IActionResult> GetEmployeesCsv()
        {
            var employees = await _context.Employees.ToListAsync();
            var csv = "Id,Name,Position,Salary\n" +
                      string.Join("\n", employees.Select(e => $"{e.Id},{e.Name},{e.Position},{e.Salary}"));
            return File(System.Text.Encoding.UTF8.GetBytes(csv), "text/csv", "employees.csv");
        }


        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        // GET: api/employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee employee)
        {
            if (employee == null || string.IsNullOrWhiteSpace(employee.Name))
                return BadRequest("Invalid employee data.");

            if (employee.Salary < 0)
                return BadRequest("Salary must be non-negative.");

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }
    }
}