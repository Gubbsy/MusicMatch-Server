namespace Abstraction.Models
{
    public class UserGenre
    {
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public string UserId { get; set; }
        public ApplicationUser AssociatedUser { get; set; }
    }
}
