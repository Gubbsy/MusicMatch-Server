using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class UserVenueDbo
    {
        public int VenueId { get; set; }
        public VenueDbo Venue { get; set; }

        public string UserId { get; set; }
        public ApplicationUserDbo AssociatedUser { get; set; }
    }
}
