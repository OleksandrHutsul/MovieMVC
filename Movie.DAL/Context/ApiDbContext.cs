using Microsoft.EntityFrameworkCore;
using Movie.DAL.Entities;

namespace Movie.DAL.Context
{
    public class ApiDbContext: DbContext
    {
        public virtual DbSet<Films> Films { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<FilmCategories> FilmCategories { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
    }
}
