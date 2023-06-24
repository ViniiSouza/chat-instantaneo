using AutoMapper;
using Chat.Application.DTOs;
using Chat.Domain.Interfaces.Services;
using Chat.Domain.Models;
using Chat.Extensions;
using Chat.Infra.Data;
using Chat.Security;

namespace Chat.Application.Services
{
    public class UserAppService : BaseAppService<UserDTO, User>, IUserAppService
    {
        public UserAppService(IMapper mapper, UnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public string? Create(CreateUserDTO dto)
        {
            if (_unitOfWork.UserRepository.GetByUserName(dto.UserName) != null)
            {
                return null;
            }
            var entity = _mapper.Map<User>(dto);
            entity.Password = entity.Password.ToShaHash();
            _unitOfWork.UserRepository.Create(entity);
            _unitOfWork.Save();
            _unitOfWork.FreeInstance(entity);
            return Login(new(dto.UserName, dto.Password));
        }

        public string? Login(UserLoginDTO dto)
        {
            dto.Password = dto.Password.ToShaHash();
            if (!_unitOfWork.UserRepository.IsUserValid(dto.UserName, dto.Password))
            {
                return null;
            }
            var entity = _mapper.Map<User>(dto);

            _unitOfWork.UserRepository.SetLastLogin(dto.UserName);
            _unitOfWork.Save();
            return TokenService.GenerateToken(entity);
        }

        public string? RequestMessage(string requesterUsername, string receiverUsername, string message)
        {
            var requester = _unitOfWork.UserRepository.GetByUserName(requesterUsername);
            var receiver = _unitOfWork.UserRepository.GetByUserName(receiverUsername);
            if (_unitOfWork.MessageRequestRepository.ExistsRequest(requester.Id, receiver.Id))
            {
                return "A message request already exists for this user!";
            }

            _unitOfWork.MessageRequestRepository.CreateRequest(requester.Id, receiver.Id, message);
            _unitOfWork.Save();
            return null;
        }
    }
}
