using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class VenueDbo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public new IEnumerable<UserVenueDbo> AssociatedUsers { get; set; }
    }
}
