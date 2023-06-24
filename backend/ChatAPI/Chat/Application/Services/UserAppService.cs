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
        private readonly IMessageRequestRepository _messageRepository;
        public UserAppService(IMapper mapper, IUserRepository repository, IMessageRequestRepository messageRepository) : base(mapper, repository)
        {
            _messageRepository = messageRepository;
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

        public string? RequestMessage(string requesterUsername, string receiverUsername, string message)
        {
            var requester = (_repository as IUserRepository).GetByUserName(requesterUsername);
            var receiver = (_repository as IUserRepository).GetByUserName(receiverUsername);
            if (_messageRepository.ExistsRequest(requester.Id, receiver.Id))
            {
                return "A message request already exists for this user!";
            }

            _messageRepository.CreateRequest(requester.Id, receiver.Id, message);
            return null;
        }
    }
}
