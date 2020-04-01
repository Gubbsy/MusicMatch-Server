namespace MusicMatch_Server.Responses
{
    public class AccountDetails
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Bio { get; set; }
        public string LookingFor { get; set; }
        public string[] Genres { get; set; }
        public string[] Venues { get; set; }
        public int MatchRadius { get; set; }
    }
}
