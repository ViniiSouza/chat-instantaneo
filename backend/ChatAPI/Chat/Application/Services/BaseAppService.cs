using AutoMapper;
using Chat.Domain.Interfaces.Repositories;
using Chat.Domain.Interfaces.Services;

namespace Chat.Application.Services
{
    public class BaseAppService<TDto, TEntity> : IBaseAppService<TDto, TEntity>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;

        public BaseAppService(IMapper mapper, IRepository<TEntity> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual void Create(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repository.Create(entity);
        }

        public virtual void Delete(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repository.Delete(entity);
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            var entities = _repository.GetAll();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual TDto GetById(int id)
        {
            var entity = _repository.GetById(id);
            return _mapper.Map<TDto>(entity);
        }

        public virtual void Update(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repository.Update(entity);
        }
    }
}
