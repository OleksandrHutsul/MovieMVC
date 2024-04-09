using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Movie.DAL.Entities.Interfaces;

namespace Movie.DAL.Entities
{
    public class Categories: IEntity
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public required string Name { get; set; }
        [Column(TypeName = "tinyint")]
        public int? ParentCategoryId { get; set; }

        public virtual ICollection<FilmCategories>? FilmCategories { get; set; }
    }
}
