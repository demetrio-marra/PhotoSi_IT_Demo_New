using Microsoft.EntityFrameworkCore;
using PhotoSi_IT_Demo_New.Infrastructure.Abstractions.DAL;

namespace PhotoSi_IT_Demo_New.Infrastructure.DAL
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext context;

        public GenericRepository(DbContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity?> Create(TEntity entity)
        {
            var r = await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return r.Entity;
        }

        public async Task Delete(int id)
        {
            var objEntity = await GetById(id);
            if (objEntity == null)
                return;

            context.Set<TEntity>().Remove(objEntity);
            await context.SaveChangesAsync();
        }

        public async Task<TEntity?> Update(TEntity entity)
        {
            var r = context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return r.Entity;
        }
    }
}
