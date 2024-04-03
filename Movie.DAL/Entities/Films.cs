using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Movie.DAL.Entities
{
    public class Films
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(200)]
        public required string Name { get; set; }
        [MaxLength(200)]
        public required string Director { get; set; }
        [DataType(DataType.Date)]
        public required DateTime Release { get; set; }

        public ICollection<FilmCategories> FilmCategories { get; set; }
    }
}
