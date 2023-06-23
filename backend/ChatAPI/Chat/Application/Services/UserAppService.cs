using AutoMapper;
using Chat.Application.DTOs;
using Chat.Domain.Interfaces.Repositories;
using Chat.Domain.Interfaces.Services;
using Chat.Domain.Models;
using Chat.Extensions;
using Chat.Security;

namespace Chat.Application.Services
{
    public class UserAppService : BaseAppService<UserDTO, User>, IUserAppService
    {
        public UserAppService(IMapper mapper, IUserRepository repository) : base(mapper, repository)
        {
        }

        public string? Create(CreateUserDTO dto)
        {
            var entity = _mapper.Map<User>(dto);
            entity.Password = entity.Password.ToShaHash();
            _repository.Create(entity);
            return Login(new(dto.UserName, dto.Password));
        }

        public string? Login(UserLoginDTO dto)
        {
            dto.Password = dto.Password.ToShaHash();
            if (!(_repository as IUserRepository).IsUserValid(dto.UserName, dto.Password))
            {
                return null;
            }
            var entity = _mapper.Map<User>(dto);

            return TokenService.GenerateToken(entity);
        }
    }
}
