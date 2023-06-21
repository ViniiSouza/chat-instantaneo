using Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infra.Contexts
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
    }
}
