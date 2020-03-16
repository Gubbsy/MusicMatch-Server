using System;
using System.Collections.Generic;
using System.Text;

namespace Abstraction.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserGenre> AssociatedUsers { get; set; }
    }
}
