using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solution.Application.Features.Products.DTOs;
using Solution.Application.Features.Products.Requests;

namespace Solution.API.Controllers
{
    [Route("")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("products", Name = "GetProducts")]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<SearchProductsRequest>> GetProduct(
            [FromQuery] string? productCode,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "ProductName",
            [FromQuery] bool descending = false)
        {

            var query = new SearchProductsRequest
            { 
                ProductCode = productCode,
                Descending = descending,
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy
            };


            var result = await _mediator.Send(query);
            if (result.Data == null)
                return NotFound(result);
            return Ok(result);
        }
    }
}
