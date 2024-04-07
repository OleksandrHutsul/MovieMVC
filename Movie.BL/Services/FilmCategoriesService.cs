using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movie.BL.Models;
using Movie.DAL.Entities;
using Movie.DAL.Extensions;
using Movie.DAL.Repositories.Interfaces;
using Movie.DAL.UnitOfWork.Interfaces;

namespace Movie.BL.Services
{
    public class FilmCategoriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<FilmCategories> _repository;
        private readonly IRepository<Films> _filmRepository;
        private readonly IRepository<Categories> _categoryRepository;
        private readonly IMapper _mapper;

        public FilmCategoriesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<FilmCategories>();
            _categoryRepository = _unitOfWork.GetRepository<Categories>();
            _filmRepository = _unitOfWork.GetRepository<Films>();
        }

        public async Task<FilmCategoriesDTO> CreateAsync(FilmCategoriesDTO newEntity)
        {
            try
            {
                var existingRecord = await _repository.GetByFilmAndCategoryAsync(newEntity.FilmsId, newEntity.CategoriesId);
                if (existingRecord != null)
                {
                    throw new DuplicateItemException("Для цього фільму категорія вже додана.");
                }

                var film = await _filmRepository.GetByIdAsync(newEntity.FilmsId);
                if (film == null)
                {
                    throw new InvalidIdException($"Фільм з ідентифікатором '{newEntity.FilmsId}' не знайдено.");
                }
                
                var category = await _categoryRepository.GetByIdAsync(newEntity.CategoriesId);
                if (category == null)
                {
                    throw new InvalidIdException($"Категорія з ідентифікатором '{newEntity.CategoriesId}' не знайдена.");
                }

                var entity = new FilmCategories
                {
                    Id = default,
                    FilmsId = newEntity.FilmsId,
                    CategoriesId = newEntity.CategoriesId
                };

                await _repository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<FilmCategoriesDTO>(entity);
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task<ICollection<FilmCategoriesDTO>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<ICollection<FilmCategoriesDTO>>(await _repository.GetAllAsync());
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task<FilmCategoriesDTO> GetByIdAsync(int id)
        {
            try
            {
                return _mapper.Map<FilmCategoriesDTO>(await _repository.GetByIdAsync(id)) ??
                    throw new InvalidIdException(ExceptionMessage(id));
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task<FilmCategoriesDTO> UpdateAsync(FilmCategoriesDTO editEntity)
        {
            try
            {
                var currentEntity = await _repository.GetByIdAsync(editEntity.Id) ??
                    throw new InvalidIdException(ExceptionMessage(editEntity.Id));

                var categoryExists = await _categoryRepository.Get()
                    .AnyAsync(x => x.Id == editEntity.CategoriesId);

                if (!categoryExists)
                    throw new InvalidIdException(ExceptionMessage(editEntity.CategoriesId));

                var filmExists = await _filmRepository.Get()
                    .AnyAsync(x => x.Id == editEntity.FilmsId);

                if (!filmExists)
                    throw new InvalidIdException(ExceptionMessage(editEntity.FilmsId));

                currentEntity.FilmsId = editEntity.FilmsId;
                currentEntity.CategoriesId = editEntity.CategoriesId;

                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<FilmCategoriesDTO>(currentEntity);
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

        private string ExceptionMessage(object? value = null) =>
            value switch
            {
                int idt when value is int => $"Фільм з id: {idt} ще/вже не існує!",
                string namet when value is string => $"Фільм з назваю {namet} вже існує",
                _ => "Something has gone wrong"
            };
    }
}
