using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movie.DAL.Entities
{
    public class Categories
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public required string Name { get; set; }
        [Column(TypeName = "tinyint")]
        public int? ParentCategoryId { get; set; }
    }
}
