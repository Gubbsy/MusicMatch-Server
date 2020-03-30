using Abstraction.Models;
using System.Collections.Generic;
using System.Linq;

namespace SQLServer.Models
{
    public class ApplicationUserDbo : ApplicationUser
    {
        public new string Name { get => base.Name; set => base.Name = value; }
        public new double Lat { get => base.Lat; set => base.Lat = value; }
        public new double Lon { get => base.Lon; set => base.Lon = value; }
        public new string Bio { get => base.Bio; set => base.Bio = value; }
        public new string LookingFor { get => base.LookingFor; set => base.LookingFor = value; }
        public new IEnumerable<UserGenreDbo> Genres { get => base.Genres.Cast<UserGenreDbo>(); set => base.Genres = value.Cast<UserGenre>(); }
        public new IEnumerable<UserVenueDbo> Venues { get => base.Venues.Cast<UserVenueDbo>(); set => base.Venues = value.Cast<UserVenue>(); }
        public new int MatchRadius { get => base.MatchRadius; set => base.MatchRadius = value; }
    }
}
