using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.NetCore.Domains;
using FluentValidation;
using Logic.Interfaces;
using Logic.Repositories;

namespace Logic.Products
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository _repository;
        private readonly IValidator<Product> _validator;

        public ProductLogic(IProductRepository repository, IValidator<Product> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<Product>>> GetAllAsync()
        {
            var products =await _repository.GetAllAsync();

            return Result.Ok(products);
        }

        public async Task<Result<Product>> GetByIdAsync(Guid id)
        {
            var product = await _repository.GetAsync(id);
            if (product == null)
            {
                return Result.Failure<Product>("Nie ma Produktu o tym Id");
            }
            return Result.Ok(product);
        }

        public async Task<Result<Product>> CreateAsync(Product product)
        {
            if (product == null)
            {
               throw new ArgumentNullException(nameof(product));
            }

            var resultValidator =_validator.Validate(product);

            if (resultValidator.IsValid == false)
            {
                return Result.Failure<Product>(resultValidator.Errors);
            }

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return Result.Ok(product);
        }

        public async Task<Result<Product>> RemoveAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

             _repository.Remove(product);
            await _repository.SaveChangesAsync();

            return Result.Ok((Product) null);

        }

        public async Task<Result<Product>> UpdateAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var resultValidator = _validator.Validate(product);
            if (resultValidator.IsValid == false)
            {
                return Result.Failure<Product>(resultValidator.Errors);
            }

            await _repository.SaveChangesAsync();


            return Result.Ok(product);
        }
    }
}