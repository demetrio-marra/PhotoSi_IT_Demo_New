namespace PhotoSi_IT_Demo_New.Infrastructure.Abstractions.DAL
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetById(int id);
        Task<TEntity?> Create(TEntity entity);
        Task<TEntity?> Update(TEntity entity);
        Task Delete(int id);
    }
}
