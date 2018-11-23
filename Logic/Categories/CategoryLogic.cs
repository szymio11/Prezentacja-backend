using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.NetCore.Domains;
using FluentValidation;
using Logic.Interfaces;
using Logic.Repositories;

namespace Logic.Categories
{
    public class CategoryLogic : BaseLogic, ICategoryLogic
    {
        private readonly IRepository<Category> _repository;
        private readonly IValidator<Category> _validator;
        private readonly IProductRepository _productRepository;

        public CategoryLogic(IRepository<Category> repository,
           IValidator<Category> validator,
           IProductRepository productRepository)
        {
            _repository = repository;
            _validator = validator;
            _productRepository = productRepository;
        }

        public async Task<Result<Category>> CreateAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var validationResult = _validator.Validate(category);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Category>(validationResult.Errors);
            }
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();

            return Result.Ok(category);
        }

        public async Task<Result<IEnumerable<Category>>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();

            return Result.Ok(categories);
        }

        public async Task<Result<Category>> UpdateAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            var validationResult = _validator.Validate(category);
            if (validationResult.IsValid == false)
            {
                return Result.Failure<Category>(validationResult.Errors);
            }
            await _repository.SaveChangesAsync();
            return Result.Ok(category);
        }

        public async Task<Result<Category>> GetAsync(Guid id)
        {
            var category = await _repository.GetAsync(id);
            if (category == null)
            {
                return Result.Failure<Category>("Nie ma kategorii o tym Id");
            }

            return Result.Ok(category);
        }

        public async Task<Result> RemoveAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            await _productRepository.DeleteByCategoryIdAsync(category.Id);

            _repository.Remove(category);
            await _repository.SaveChangesAsync();
            return Result.Ok();
        }
    }
}