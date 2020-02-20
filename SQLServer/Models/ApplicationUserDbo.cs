using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class ApplicationUserDbo : IdentityUser
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Bio { get; set; }
        public string LookingFor { get; set; }
        public IEnumerable<UserGenreDbo> Genres { get; set; }
        public IEnumerable<UserVenueDbo> Venues { get; set; }
        public int MatchRadius { get; set; }
    }
}
