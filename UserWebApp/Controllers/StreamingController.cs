using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserWebApp.Models;

namespace UserWebApp.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class StreamingController : ControllerBase
    {
        private readonly UniversityContext _context;

        public StreamingController(UniversityContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Streaming stream)
        {
            await _context.Streams.AddAsync(stream);    
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
