namespace Chat.Domain.Models
{
    public class User : EntityBase
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
