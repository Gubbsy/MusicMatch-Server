using Abstraction.Models;

namespace SQLServer.Models
{
    public class UserVenueDbo : UserVenue
    {
        public new int VenueId { get => base.VenueId; set => base.VenueId = value; }
        public new VenueDbo Venue { get => (VenueDbo)base.Venue; set => base.Venue = value; }

        public new string UserId { get; set; }
        public new ApplicationUserDbo AssociatedUser { get; set; }
    }
}
