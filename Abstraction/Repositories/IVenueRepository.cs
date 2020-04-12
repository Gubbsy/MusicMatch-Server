using Abstraction.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstraction.Repositories
{
    public interface IVenueRepository
    {
        public Task<IEnumerable<Venue>> GetAllVenues();
        public Task VenueAdditions(string[] venues, ApplicationUser user);
    }
}
