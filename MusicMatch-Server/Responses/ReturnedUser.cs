namespace Abstraction.Models
{
    public class ReturnedUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public string Bio { get; set; }
        public string LookingFor { get; set; }
        public string[] Genres { get; set; }
        public string[] Venues { get; set; }
        public string Role { get; set; }
    }
}
