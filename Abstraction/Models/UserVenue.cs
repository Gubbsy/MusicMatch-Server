using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class UserVenue
    {
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
        public string UserId { get; set; }
        public ApplicationUser AssociatedUser { get; set; }
    }
}
