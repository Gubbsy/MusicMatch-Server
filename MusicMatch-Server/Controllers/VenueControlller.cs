using Abstraction.Models;
using Abstraction.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Controllers
{
    public class VenueControlller : APIControllerBase
    {
        private readonly IVenueRepository venueRepository;

        public VenueControlller(IVenueRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }

        [HttpPost(Endpoints.Venues + "getallvenues")]
        public async Task<ObjectResult> GetAllVenues()
        {
            IEnumerable<Venue> venues = await venueRepository.GetAllVenues();

            return Ok(new Responses.AllVenues
            {
                Venues = venues.Select(v => v.Name).ToArray()
            });
        }
    }
}
