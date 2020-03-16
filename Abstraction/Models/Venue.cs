using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserVenue> AssociatedUsers { get; set; }
    }
}
