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
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<FilmCategories> GetByFilmAndCategoryAsync(int filmsId, int categoryId);
        Task<ICollection<Films>> GetAllWithCategoriesAsync();
        Task<ICollection<Categories>> GetAllWithFilmsAsync();
    }
}
