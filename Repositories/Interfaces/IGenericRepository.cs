using System.Linq.Expressions;

namespace RakbnyMa_aak.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetAllQueryable();
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> CompleteAsync();
        IQueryable<T> GetByIdQueryable(object id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}