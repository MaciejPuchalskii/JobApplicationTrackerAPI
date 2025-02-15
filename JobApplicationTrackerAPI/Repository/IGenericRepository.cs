namespace JobApplicationTrackerAPI.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        public void Add(T entity);

        public void Delete(T entity);

        public void Update(T entity);

        public List<T> GetAll(T entity);

        public T GetById(int id);
    }
}