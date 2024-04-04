using AutoMapper;
using Movie.BL.Models;
using Movie.DAL.Entities;

namespace Movie.BL.AuthoMapper
{
    public class DbDtoMappingProfile: Profile
    {
        public DbDtoMappingProfile()
        {
            CreateMap<Categories, CategoriesDTO>().ReverseMap();
            CreateMap<FilmCategories, FilmCategoriesDTO>().ReverseMap();
            CreateMap<Films, FilmsDTO>().ReverseMap();
        }
    }
}