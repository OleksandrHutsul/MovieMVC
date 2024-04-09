using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie.BL.Models;
using Movie.DAL.Entities;
using Movie.DAL.Extensions;
using Movie.DAL.Repositories.Interfaces;
using Movie.DAL.UnitOfWork.Interfaces;

namespace Movie.BL.Services
{
    public class FilmService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Films> _repository;
        private readonly IMapper _mapper;
        public FilmService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Films>();
        }

        public async Task<FilmsDTO> CreateAsync(FilmsDTO newEntity)
        {
            try
            {
                var existsName = await _repository.Get()
                    .AnyAsync(x => x.Name.ToUpper().Trim() == newEntity.Name.ToUpper().Trim());

                var existsDirector = await _repository.Get()
                    .AnyAsync(x => x.Director.ToUpper().Trim() == newEntity.Director.ToUpper().Trim());

                var existsRelease = await _repository.Get()
                    .AnyAsync(x => x.Release == newEntity.Release);

                var exists = existsName && existsDirector && existsRelease;

                if (exists)
                    throw new DuplicateItemException(ExceptionMessage(newEntity.Name));

                var entity = new Films
                {
                    Id = default,
                    Name = newEntity.Name.Trim(),
                    Director = newEntity.Director.Trim(),
                    Release = newEntity.Release
                };

                await _repository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<FilmsDTO>(entity);
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task<ICollection<FilmsDTO>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<ICollection<FilmsDTO>>(await _repository.GetAllAsync());
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task<FilmsDTO> GetByIdAsync(int id)
        {
            try
            {
                return _mapper.Map<FilmsDTO>(await _repository.GetByIdAsync(id)) ??
                    throw new InvalidIdException(ExceptionMessage(id));
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task<FilmsDTO> UpdateAsync(FilmsDTO editEntity)
        {
            try
            {
                var currentEntity = await _repository.GetByIdAsync(editEntity.Id) ??
                    throw new InvalidIdException(ExceptionMessage(editEntity.Id));

                var tagExists = await _repository.Get()
                    .AnyAsync(x => (x.Name.ToUpper().Trim() == editEntity.Name.ToUpper().Trim()) && 
                    (x.Director.ToUpper().Trim() == editEntity.Director.ToUpper().Trim()) &&
                    (x.Release == editEntity.Release));

                if (tagExists)
                    throw new DuplicateItemException(ExceptionMessage(editEntity.Name));

                _mapper.Map(editEntity, currentEntity);
                await _unitOfWork.SaveChangesAsync();
                return editEntity;
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var currentEntity = await _repository.GetByIdAsync(id) ??
                    throw new InvalidIdException(ExceptionMessage(id));
                await _repository.DeleteAsync(currentEntity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task<ICollection<FilmsDTO>> GetAllWithCategoriesAsync()
        {
            var films = await _repository.GetAllWithCategoriesAsync();
            var filmsDTO = _mapper.Map<ICollection<FilmsDTO>>(films);

            foreach (var filmDTO in filmsDTO)
            {
                var categories = films
                    .Where(f => f.Id == filmDTO.Id)  
                    .SelectMany(f => f.FilmCategories)  
                    .Select(fc => fc.Categories.Name)  
                    .ToList();

                filmDTO.Categories = categories;
            }

            return filmsDTO;
        }

        private string ExceptionMessage(object? value = null) =>
            value switch
            {
                int idt when value is int => $"Фільм з id: {idt} ще/вже не існує!",
                string namet when value is string => $"Фільм з назваю {namet} вже існує",
                _ => "Something has gone wrong"
            };
    }
}
