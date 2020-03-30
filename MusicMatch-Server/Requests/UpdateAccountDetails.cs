namespace MusicMatch_Server.Requests
{
    public class UpdateAccountDetails
    {
        public string[] Genres { get; set; }
        public string[] Venues { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string LookingFor { get; set; }
        public int MatchRadius { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
