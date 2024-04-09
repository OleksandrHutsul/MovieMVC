using Movie.DAL.Entities.Interfaces;
using Movie.DAL.Repositories.Interfaces;

namespace Movie.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
        void SaveChanges();
        Task SaveChangesAsync();
    }
}