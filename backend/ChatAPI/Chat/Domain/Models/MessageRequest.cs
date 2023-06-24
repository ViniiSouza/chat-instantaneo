namespace Chat.Domain.Models
{
    public class MessageRequest : EntityBase
    {
        public User Requester { get; set; }

        public int RequesterId { get; set; }

        public User Receiver { get; set; }

        public int ReceiverId { get; set; }

        public string? CustomInvite { get; set; }
    }
}
