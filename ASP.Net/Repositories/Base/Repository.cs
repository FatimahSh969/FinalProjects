using Microsoft.EntityFrameworkCore;
using SmartAirPort.Data;

namespace SmartAirPort.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AirPortDBContext _context;
        private readonly DbSet<T> _dbset;

        public Repository(AirPortDBContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public void Add(T obj)
        {
            _dbset.Add(obj);
            Save();
        }

        public void Delete(T obj)
        {
            _dbset.Remove(obj);
            Save();
        }

        public void Update(T obj)
        {
            _dbset.Update(obj);
            Save();
        }

        public T GetById(int id)
        {
            return _dbset.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }

        private void Save()
        {
            _context.SaveChanges();

        }

    }
}
