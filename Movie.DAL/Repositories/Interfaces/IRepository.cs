using Movie.DAL.Entities;
using Movie.DAL.Specifications;
using System.Linq.Expressions;

namespace Movie.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get();
        Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity>? spec = null);
        Task<TEntity?> GetByIdAsync(object id);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(ICollection<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(ICollection<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(object id);
        Task DeleteRangeAsync(ICollection<TEntity> entities);
        Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<FilmCategories> GetByFilmAndCategoryAsync(int filmsId, int categoryId);
    }
}
