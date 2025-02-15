using JobApplicationTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationTrackerAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public DbSet<T> entity => _context.Set<T>();

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<T> GetAll(T entity)
        {
            return this.entity.ToList();
        }

        public T GetById(int id)
        {
            var entity = _context.Find<T>(id);
            return entity;
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}