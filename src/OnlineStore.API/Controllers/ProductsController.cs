using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Helpers;
using OnlineStore.BusinessLogic.Commands;
using OnlineStore.BusinessLogic.Contracts.Dtos;
using OnlineStore.BusinessLogic.Contracts.Dtos.Product;
using OnlineStore.BusinessLogic.Handlers;
using OnlineStore.BusinessLogic.Queries;
using OnlineStore.Data.Contracts.Entities.Products;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieve a list of products.
        /// </summary>
        /// <returns></returns>
        [HttpGet] // api/products
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] ProductGetAllRequestDto productGetAllRequestDto)
        {
            var result = await _mediator.Send(new GetProductsQuery { ProductGetAllRequestDto = productGetAllRequestDto });
            Response.Headers.Add("X-Pagination", result.GetMetadata());
            return Ok(ApiResult<IEnumerable<ProductDto>>.Success(result));
        }

        /// <summary>
        /// Retrieve a single product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}", Name = "GetByIdAsync")] // api/products/{productId}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int productId)
        {
            var result = await _mediator.Send(new GetProductByIdQuery() { Id = productId });
            return Ok(ApiResult<ProductDto>.Success(result));
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="createProductDto"></param>
        /// <returns></returns>
        [HttpPost] // api/products
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProductDto createProductDto)
        {
            var result = await _mediator.Send(new CreateProductCommand() { CreateProductDto = createProductDto});
            return CreatedAtAction(
                nameof(GetByIdAsync), 
                new { productId = result.Id }, 
                ApiResult<ProductDto>.Success(result));
        }

        /// <summary>
        /// Update a product.
        /// </summary>
        /// <param name="updateProductDto"></param>
        /// <returns></returns>
        [HttpPut] // api/products
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductDto updateProductDto)
        {
            var result = await _mediator.Send(new UpdateProductCommand() { UpdateProductDto = updateProductDto });
            return Ok(ApiResult<ProductDto>.Success(result));
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("{productId}")] // api/products/{productId}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int productId)
        {
            await _mediator.Send(new DeleteProductCommand() { Id = productId});
            return Ok(ApiResult<string>.Success(string.Empty));
        }
    }   
}
