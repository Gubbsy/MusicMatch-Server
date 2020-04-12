using Abstraction.Models;
using System.Collections.Generic;
using System.Linq;

namespace SQLServer.Models
{
    public class GenreDbo : Genre
    {
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Name { get => base.Name; set => base.Name = value; }
        public new IEnumerable<UserGenreDbo> AssociatedUsers { get => base.AssociatedUsers.Cast<UserGenreDbo>(); set => base.AssociatedUsers = value.Cast<UserGenre>(); }
    }
}
