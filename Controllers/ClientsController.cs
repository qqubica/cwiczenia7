using cwiczenia7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cwiczenia7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ITripService _service;
        public ClientsController(ITripService service)
        {
            _service = service;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (await _service.ClientHasTrips(id))
                return BadRequest();
            return Ok(_service.DeleteClient(id));
        }
    }
}
