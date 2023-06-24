using Chat.Application.DTOs;
using Chat.Domain.Models;

namespace Chat.Domain.Interfaces.Services
{
    public interface IUserAppService : IBaseAppService<UserDTO, User>
    {
        string? Create(CreateUserDTO dto);

        string? Login(UserLoginDTO dto);

        string? RequestMessage(string requesterUsername, string receiverUsername, string message);
    }
}
