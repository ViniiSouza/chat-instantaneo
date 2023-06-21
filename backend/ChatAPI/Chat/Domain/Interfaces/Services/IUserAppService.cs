using Chat.Application.DTOs;
using Chat.Domain.Models;

namespace Chat.Domain.Interfaces.Services
{
    public interface IUserAppService : IBaseAppService<UserDTO, User>
    {
    }
}
