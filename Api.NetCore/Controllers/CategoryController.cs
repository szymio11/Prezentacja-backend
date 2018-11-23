using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.NetCore.Domains;
using Api.NetCore.Infrastructure;
using Api.NetCore.ModelsDto.Categories;
using AutoMapper;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.NetCore.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryLogic _categoryLogic;

        public CategoryController(IMapper mapper,
            ICategoryLogic categoryLogic)
        {
            _mapper = mapper;
            _categoryLogic = categoryLogic;
        }


        /// <summary>
        /// Get Categories.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryLogic.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CategoryDto>>(categories.Value);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _categoryLogic.GetAsync(id);
            if (category.Success == false)
            {
                return NotFound(category.Errors);
            }

            var result = _mapper.Map<CategoryDto>(category.Value);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CategoryDto viewModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(viewModel);

            var result = await _categoryLogic.CreateAsync(category);
            if (result.Success == false)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(result.Errors);
            }

            var resultMap = _mapper.Map<CategoryDto>(result.Value);
            return CreatedAtAction(nameof(GetById), new {id = category.Id}, resultMap);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryDto categoryView)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var category = await _categoryLogic.GetAsync(id);
            if (category.Success == false)
            {
                return NotFound(category.Errors);
            }

            _mapper.Map(categoryView, category.Value);
            var result = await _categoryLogic.UpdateAsync(category.Value);
            if (result.Success == false)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryLogic.GetAsync(id);
            if (category.Success == false)
            {
                return NotFound(category.Errors);
            }

            var result = await _categoryLogic.RemoveAsync(category.Value);
            if (result.Success == false)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}