﻿using Microsoft.EntityFrameworkCore;
using Movie.DAL.Context;
using Movie.DAL.Repositories.Interfaces;
using Movie.DAL.Specifications;
using System.Linq.Expressions;

namespace Movie.DAL.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApiDbContext _context;

        public GenericRepository(ApiDbContext context) => _context = context;

        public IQueryable<TEntity> Get() => _context.Set<TEntity>();

        public void Add(TEntity entity) => _context.Set<TEntity>().Add(entity);

        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

        public void AddRange(ICollection<TEntity> entities) => _context.Set<TEntity>().AddRange(entities);

        public async Task AddRangeAsync(ICollection<TEntity> entities) => await _context.AddRangeAsync(entities);

        public void DeleteById(object id)
        {
            var entity = GetById(id);
            if (entity != null)
                Delete(entity);
        }

        public async Task DeleteByIdAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                Delete(entity);
        }

        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public async Task DeleteAsync(TEntity entity) => await Task.Run(() => Delete(entity));

        public void DeleteRange(ICollection<TEntity> entities) => _context.Set<TEntity>().RemoveRange(entities);

        public async Task DeleteRangeAsync(ICollection<TEntity> entities) =>
            await Task.Run(() => _context.Set<TEntity>().RemoveRange(entities));

        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate) =>
            _context.Set<TEntity>().Where(predicate).ToList();

        public async Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) =>
            await _context.Set<TEntity>().Where(predicate).ToListAsync();

        public ICollection<TEntity> GetAll(ISpecification<TEntity>? spec = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (spec != null)
            {
                query = ApplySpecification(query, spec);
            }

            return query.ToList();
        }

        public async Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity>? spec = null)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (spec != null)
            {
                query = ApplySpecification(query, spec);
            }

            return await query.ToListAsync();
        }

        public TEntity? GetById(object id) => _context.Set<TEntity>().Find(id);

        public async Task<TEntity?> GetByIdAsync(object id) => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);

        public async Task UpdateAsync(TEntity entity) => await Task.Run(() => Update(entity));

        public void UpdateRange(ICollection<TEntity> entities) => _context.Set<TEntity>().UpdateRange(entities);

        public async Task UpdateRangeAsync(ICollection<TEntity> entities) =>
            await Task.Run(() => _context.Set<TEntity>().UpdateRange(entities));

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
    }
}
