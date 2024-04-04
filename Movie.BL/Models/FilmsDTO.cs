using System.ComponentModel.DataAnnotations;

namespace Movie.BL.Models
{
    public class FilmsDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        [Required, MaxLength(200)]
        public string Director { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime Release { get; set; }

        public ICollection<FilmCategoriesDTO> FilmCategories { get; set; }
    }
}
