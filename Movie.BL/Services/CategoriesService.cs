using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Movie.BL.Models;
using Movie.DAL.Entities;
using Movie.DAL.Extensions;
using Movie.DAL.Repositories.Interfaces;
using Movie.DAL.UnitOfWork.Interfaces;

namespace Movie.BL.Services
{
    public class CategoriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Categories> _repository;
        private readonly IMapper _mapper;
        public CategoriesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Categories>();
        }

        public async Task<CategoriesDTO> CreateAsync(CategoriesDTO newEntity)
        {
            try
            {
                var exists = await _repository.Get()
                    .AnyAsync(x => x.Name.ToUpper().Trim() == newEntity.Name.ToUpper().Trim());

                if (exists)
                    throw new DuplicateItemException(ExceptionMessage(newEntity.Name));

                var entity = new Categories
                {
                    Id = default,
                    Name = newEntity.Name.Trim(),
                    ParentCategoryId = newEntity.ParentCategoryId
                };

                await _repository.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
                return _mapper.Map<CategoriesDTO>(entity);
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task<ICollection<CategoriesDTO>> GetAllAsync()
        {
            try
            {
                return _mapper.Map<ICollection<CategoriesDTO>>(await _repository.GetAllAsync());
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task<CategoriesDTO> GetByIdAsync(int id)
        {
            try
            {
                return _mapper.Map<CategoriesDTO>(await _repository.GetByIdAsync(id)) ??
                    throw new InvalidIdException(ExceptionMessage(id));
            }
            catch (DbUpdateException ex)
            {
                throw new ServerErrorException(ex.Message, ex);
            }
        }

        public async Task<CategoriesDTO> UpdateAsync(CategoriesDTO editEntity)
        {
            try
            {
                var currentEntity = await _repository.GetByIdAsync(editEntity.Id) ??
                    throw new InvalidIdException(ExceptionMessage(editEntity.Id));

                var tagExists = await _repository.Get()
                    .AnyAsync(x => x.Name.ToUpper().Trim() == editEntity.Name.ToUpper().Trim());

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

        private string ExceptionMessage(object? value = null) =>
            value switch
            {
                int idt when value is int => $"Категорії з id: {idt} ще/вже не існує!",
                string namet when value is string => $"Категорія з назваю {namet} вже існує",
                _ => "Something has gone wrong"
            };
    }
}
