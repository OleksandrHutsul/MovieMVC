using Microsoft.EntityFrameworkCore;
using Movie.DAL.Context;
using Movie.DAL.Entities;
using Movie.DAL.Entities.Interfaces;
using Movie.DAL.Extensions;
using Movie.DAL.Repositories.Interfaces;
using Movie.DAL.Specifications;

namespace Movie.DAL.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ApiDbContext _context;

        public GenericRepository(ApiDbContext context) => _context = context;

        public IQueryable<TEntity> Get() => _context.Set<TEntity>();

        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

        public async Task DeleteAsync(TEntity entity) => await Task.Run(() => _context.Set<TEntity>().Remove(entity));

        public async Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity>? spec = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (spec != null)
            {
                query = ApplySpecification(query, spec);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object id) => await _context.Set<TEntity>().FindAsync(id);

        public async Task UpdateAsync(TEntity entity) => await Task.Run(() => _context.Set<TEntity>().Update(entity));

        private IQueryable<TEntity> ApplySpecification(IQueryable<TEntity> query, ISpecification<TEntity> spec)
        {
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            return query;
        }

        public async Task<FilmCategories> GetByFilmAndCategoryAsync(int filmsId, int categoryId) => await _context.FilmCategories
            .FirstOrDefaultAsync(fc => fc.FilmsId == filmsId && fc.CategoriesId == categoryId);

        public async Task<ICollection<Films>> GetAllWithCategoriesAsync()
        {
            try
            {
                return await _context.Films
                    .Include(f => f.FilmCategories)
                        .ThenInclude(fc => fc.Categories)
                    .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task<ICollection<Categories>> GetAllWithFilmsAsync()
        {
            try
            {
                return await _context.Categories
                    .Include(f => f.FilmCategories)
                        .ThenInclude(fc => fc.Films)
                    .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }
    }
}
