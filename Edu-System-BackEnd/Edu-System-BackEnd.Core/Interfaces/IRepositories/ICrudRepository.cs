namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.Interfaces
{
    public interface ICrudRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(Guid id);
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(Guid id);
    }
}
