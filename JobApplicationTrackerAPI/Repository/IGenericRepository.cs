namespace JobApplicationTrackerAPI.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        public Task Add(T entity);

        public Task Delete(T entity);

        public Task Update(T entity);

        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(Guid id);
    }
}