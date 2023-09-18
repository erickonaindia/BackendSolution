using MediatR;
using Solution.Application.Features.Categories.DTOs;
using Solution.Application.Features.Products.Responses;

namespace Solution.Application.Features.Products.Requests
{
    public class CreateCategoryRequest : IRequest<CreateCategoryResponse>
    {
        public CreateCategoryDTO CreateCategoryDTO { get; set; }

    }
}