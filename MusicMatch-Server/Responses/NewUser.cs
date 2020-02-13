using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMatch_Server.Responses
{
    public class NewUser
    {
        public string Id { get; set; }
        public string AccountRole { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Bio { get; set; }
        public string LookingFor { get; set; }
        public string[] Genres { get; set; }
        public string[] Venues { get; set; }
        public int MatchRadius { get; set; }
    }
}
