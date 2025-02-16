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

        public async Task Add(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entity.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task Update(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}