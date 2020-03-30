using System.Collections.Generic;

namespace Abstraction.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserVenue> AssociatedUsers { get; set; }
    }
}
