
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using trelloClone.Application.Repositories;
using trelloClone.Persistence.Context;

namespace trelloClone.Persistence.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, new()

    {
        private readonly TrelloCloneDbContext context;
        public EntityRepository(TrelloCloneDbContext dbContext)
        {
            context = dbContext;
        }
        public DbSet<T> Table => context.Set<T>();

        public T Add(T entity)
        {
            Table.Add(entity);
            return entity;           
        }
         public async Task <T> AddAsync(T entity)
         {
            await Table.AddAsync(entity);
            return entity;           
         }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }
        
        public async  Task<T?> GetAsync(Expression<Func<T, bool>> filter)
        {
            var x = await Table
                     .Where(filter)
                     .FirstOrDefaultAsync();

            if (x == null)
                return null;
            return x;
        }

        public async Task<IEnumerable<T>> GetIEnumerableListAsync(Expression<Func<T, bool>>? filter = null)
        { 
            return filter == null ? await Table.ToListAsync() :
                                   await Table.Where(filter).ToListAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? await Table.ToListAsync() :
                                    await Table.Where(filter).ToListAsync();
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }
        public async Task<int> SaveAsync() => await context.SaveChangesAsync();

    }
}
