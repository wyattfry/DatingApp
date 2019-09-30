using System.Threading.Tasks;
using DatingApp.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// main feature of ApiController attribute: requires routing specified via Attributes, not the old fashioned way
// it also helps with validating requests. ControllerBase is relatively new, allows access to http responses
// and actions, e.g. IActionResult. Alternative is "Controller", which has View support. Base does not have.

namespace DatingApp.Api.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            this._context = context;

        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetAllValues()
        {
            // IActionResult allows http responses rather than just text when using ActionResult
            // gets all rows in Values table
            var values = await _context.Values.ToListAsync();

            return Ok(values);
        }

        // GET api/values/5
        // [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);

            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
