using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedDomain;
using SharedDomain.Contracts;
using LoggerService;
using AspNetCoreFactory.Api.DataTransferObjects;

namespace AspNetCoreFactory.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProductController(IServiceManager serviceManager, ILoggerManager logger, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _serviceManager.Product.GetAllProductsAsync(trackChanges: false);
            var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDto);
        }
        [HttpGet("{id}", Name = "ProductById")]
        public async Task<IActionResult> GetProduct(int id)
        {
            return await GetProductById(id);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductForCreateDto product)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model state for the ProductForCreateDto object");
                return UnprocessableEntity(ModelState);
            }
            if (product == null)
            {
                _logger.LogError("ProductForCreateDto object sent from client is null. ");
                return BadRequest("ProductForCreateDto object is null");
            }
            var productEntity = _mapper.Map<Product>(product);
            await _serviceManager.Product.CreateProductAsync(productEntity);
            await _serviceManager.SaveAsync();
            var productToReturn = _mapper.Map<ProductDto>(productEntity);
            return CreatedAtRoute("ProductById", new { id = productToReturn.Id }, productToReturn);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductForUpdateDto product)
        {
            if (product == null)
            {
                _logger.LogError($"ProductForUpdateDto object sent from client is null. ");
                return BadRequest("ProductForUpdateDto object is null");
            }
            var productEntity = await _serviceManager.Product.GetProductAsync(id, trackChanges: false);
            if (productEntity == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(product, productEntity);
            _serviceManager.Product.UpdateProduct(productEntity);
            _serviceManager.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _serviceManager.Product.GetProductAsync(id, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _serviceManager.Product.DeleteProduct(product);
            _serviceManager.Save();
            return NoContent();
        }
        #region Handlers
        private async Task<IActionResult> GetProductById(int id)
        {
            var product = await _serviceManager.Product.GetProductAsync(id, trackChanges: false);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var productDto = _mapper.Map<ProductDto>(product);
                return Ok(productDto);
            }
        }
        #endregion
    }
}

