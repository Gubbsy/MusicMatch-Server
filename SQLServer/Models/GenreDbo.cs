using System;
using System.Collections.Generic;
using System.Text;

namespace SQLServer.Models
{
    public class GenreDbo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public new IEnumerable<UserGenreDbo> AssociatedUsers { get; set; }
    }
}
