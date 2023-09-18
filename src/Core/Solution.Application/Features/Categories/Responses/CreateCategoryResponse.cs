using Solution.Application.Common;
using Solution.Application.Features.Categories.DTOs;

namespace Solution.Application.Features.Products.Responses
{
    public class CreateCategoryResponse : BaseResponse
    {
        public CreateCategoryDTO Data { get; set; }
    }
}
