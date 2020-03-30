using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Abstraction.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Bio { get; set; }
        public string LookingFor { get; set; }
        public IEnumerable<UserGenre> Genres { get; set; }
        public IEnumerable<UserVenue> Venues { get; set; }
        public int MatchRadius { get; set; }
    }
}
