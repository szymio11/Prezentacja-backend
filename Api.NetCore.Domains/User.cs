namespace Api.NetCore.Domains
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
   
}