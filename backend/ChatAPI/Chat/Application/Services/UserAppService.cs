using AutoMapper;
using Chat.Application.DTOs;
using Chat.Domain.Interfaces.Repositories;
using Chat.Domain.Interfaces.Services;
using Chat.Domain.Models;

namespace Chat.Application.Services
{
    public class UserAppService : BaseAppService<UserDTO, User>, IBaseAppService<UserDTO, User>
    {
        public UserAppService(IMapper mapper, IRepository<User> repository) : base(mapper, repository)
        {
        }
    }
}
