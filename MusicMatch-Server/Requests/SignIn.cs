namespace MusicMatch_Server.Requests
{
    public class SignIn
    {
        public string Credential { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
