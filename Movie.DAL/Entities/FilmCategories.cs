﻿using Movie.DAL.Entities.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.DAL.Entities
{
    public class FilmCategories: IEntity
    {
        public int Id { get; set; }
        public int FilmsId { get; set; }
        public virtual Films Films { get; set; }
        public int CategoriesId { get; set; }
        public virtual Categories Categories { get; set; }
    }
}
