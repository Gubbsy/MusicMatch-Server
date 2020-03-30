using System.Collections.Generic;

namespace Abstraction.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserGenre> AssociatedUsers { get; set; }
    }
}
