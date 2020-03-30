using Abstraction.Models;
using System.Collections.Generic;
using System.Linq;

namespace SQLServer.Models
{
    public class VenueDbo : Venue
    {
        public new int Id { get => base.Id; set => base.Id = value; }
        public new string Name { get => base.Name; set => base.Name = value; }
        public new IEnumerable<UserVenueDbo> AssociatedUsers { get => base.AssociatedUsers.Cast<UserVenueDbo>(); set => base.AssociatedUsers = value.Cast<UserVenueDbo>(); }
    }
}
