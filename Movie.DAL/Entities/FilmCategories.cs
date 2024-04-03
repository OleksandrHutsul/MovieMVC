using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.DAL.Entities
{
    public class FilmCategories
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "tinyint")]
        public int FilmId { get; set; }
        public virtual Films Films { get; set; }
        [Column(TypeName = "tinyint")]
        public int CategoryId { get; set; }
        public virtual Categories Categories { get; set; }
    }
}
