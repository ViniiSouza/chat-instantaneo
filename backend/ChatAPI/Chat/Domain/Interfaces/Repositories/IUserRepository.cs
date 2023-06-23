using Chat.Domain.Models;

namespace Chat.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserValid(string username, string password);
    }
}
