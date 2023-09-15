using MediatR;
using Microsoft.AspNetCore.Mvc;
using Solution.Application.Features.Categories.DTOs;
using Solution.Application.Features.Products.Requests;

namespace Solution.API.Controllers
{
    [Route("")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("categories", Name = "GetCategories")]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<SearchCategoriesRequest>> GetCategories(
            [FromQuery] string? categoryCode,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "Name",
            [FromQuery] bool descending = false)
        {

            var query = new SearchCategoriesRequest
            {
                ProductCode = categoryCode,
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
