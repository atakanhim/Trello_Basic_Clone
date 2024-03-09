
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace trelloClone.Application.Repositories
{
    public interface IEntityRepository<T> where T : class
    {
        // get islemleri
        DbSet<T> Table { get; }
        Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null);
        Task<IEnumerable<T>> GetIEnumerableListAsync(Expression<Func<T, bool>>? filter = null);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter);
        Task<int> SaveAsync();

        // post islemleri
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
