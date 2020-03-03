using Abstraction.Models;
using Microsoft.AspNetCore.Mvc;
using SQLServer.Models;
using SQLServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Controllers
{
    public class VenueControlller : APIControllerBase
    {
        private readonly VenueRepository venueRepository;

        public VenueControlller(VenueRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }

        [HttpPost(Endpoints.Venues + "getallvenues")]
        public async Task<ObjectResult> GetAllVenues()
        {
            IEnumerable<Venue> venueDbos = await venueRepository.GetAllVenues();

            return Ok(new Responses.AllVenues
            {
                Venues = venueDbos.Select(v => v.Name).ToArray()
            }); ;
        }
    }
}
