using Movie.DAL.Repositories.Interfaces;
using Movie.DAL.Repositories;
using Movie.DAL.UnitOfWork.Interfaces;
using Movie.DAL.UnitOfWork;
using Movie.BL.Services;

namespace Movie.UI.DI
{
    public static class DependencyInjections
    {
        public static void AddDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<CategoriesService>();
            services.AddTransient<FilmCategoriesService>();
            services.AddTransient<FilmService>();
        }
    }
}