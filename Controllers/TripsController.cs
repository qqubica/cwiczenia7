using cwiczenia7.Models.DTO;
using cwiczenia7.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cwiczenia7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _service;
        public TripsController(ITripService service)
        {
            _service = service; 
        }
        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            return Ok(await _service.GetTips());
        }
        [HttpPost("{id}/clients")]
        public async Task<IActionResult> PostTrips(PostTrip postTrip)
        {
            int clientId;

            if (!await _service.ClientExists(postTrip.Pesel))
                clientId = (await _service.PostClient(new PostClient
                {
                    FirstName = postTrip.FirstName,
                    LastName = postTrip.LastName,
                    Email = postTrip.Email,
                    Pesel = postTrip.Pesel,
                    Telephone = postTrip.Telephone
                })).IdClient;
            else
                clientId = _service.GetClient(postTrip.Pesel).Result.IdClient;
            if (!await _service.TripExists(postTrip.IdTrip))
                return BadRequest();
            if (!await _service.ClientTripEntry(postTrip.IdTrip,clientId))
                return BadRequest();
            return Ok(_service.PostClientTrip(new PostClientTrip
            {
                IdClient = clientId,
                IdTrip = postTrip.IdTrip
            }));
        }
    }
}
