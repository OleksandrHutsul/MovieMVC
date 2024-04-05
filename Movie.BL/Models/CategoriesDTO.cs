using System.ComponentModel.DataAnnotations;

namespace Movie.BL.Models
{
    public class CategoriesDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
