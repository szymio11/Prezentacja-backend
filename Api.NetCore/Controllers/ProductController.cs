using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.NetCore.Domains;
using Api.NetCore.Hubs;
using Api.NetCore.Infrastructure;
using Api.NetCore.ModelsDto.Products;
using AutoMapper;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Api.NetCore.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductLogic _logic;
        private readonly IHubContext<NotificationHub> _hubContext;

        public ProductController(IMapper mapper,
            IProductLogic logic,
            IHubContext<NotificationHub> hubContext)
        {
            _mapper = mapper;
            _logic = logic;
            _hubContext = hubContext;
        }

        /// <summary>
        /// Create Product.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productDto);
            var result = await _logic.CreateAsync(product);
            if (result.Success == false)
            {
                result.AddErrorToModelState(ModelState);
                return BadRequest(result.Errors);
            }

            var resultDto = _mapper.Map<GetProductsDto>(result.Value);
            await _hubContext.Clients.All.SendAsync("NotificationCreate", resultDto);
            return CreatedAtAction(nameof(GetById),
                new {id = resultDto.Id},
                resultDto);
        }

        /// <summary>
        /// Update Product.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto productDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var product = await _logic.GetByIdAsync(id);
            if (product.Success == false)
            {
                return NotFound(product.Errors);
            }

            _mapper.Map(productDto, product.Value);

            var result = await _logic.UpdateAsync(product.Value);
            if (result.Success == false)
            {
                product.AddErrorToModelState(ModelState);
                return BadRequest(product.Errors);
            }

            return Ok();
        }

        /// <summary>
        /// Get all Products.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _logic.GetAllAsync();
            var result = _mapper.Map<IEnumerable<GetProductsDto>>(products.Value);
            return Ok(result);
        }

        /// <summary>
        /// Get Product by Id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _logic.GetByIdAsync(id);

            if (product.Success == false)
            {
                return NotFound(product.Errors);
            }

            var result = _mapper.Map<ProductDto>(product.Value);

            return Ok(result);
        }

        /// <summary>
        /// Delete Product.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _logic.GetByIdAsync(id);
            if (product.Success == false)
            {
                return NotFound(product.Errors);
            }
            var resultDto = _mapper.Map<GetProductsDto>(product.Value);
            await _hubContext.Clients.All.SendAsync("NotificationRemove", resultDto);
            await _logic.RemoveAsync(product.Value);
            
            return Ok();
        }
    }
}