using Chat.Domain.Interfaces.Repositories;
using Chat.Domain.Models;
using Chat.Infra.Contexts;

namespace Chat.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly ChatDbContext _context;

        public Repository(ChatDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(find => find.Id == id);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
