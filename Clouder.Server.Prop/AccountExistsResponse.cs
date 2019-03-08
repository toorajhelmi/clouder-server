namespace Clouder.Server.Entity
{
    public class AccountExistsResponse
    {
        public bool UsernameTaken { get; set; }
        public bool EmailTaken { get; set; }
    }
}
