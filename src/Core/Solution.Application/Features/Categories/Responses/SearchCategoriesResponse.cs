using Solution.Application.Common;
using Solution.Application.Features.Categories.DTOs;

namespace Solution.Application.Features.Products.Responses
{
    public class SearchCategoriesResponse : BaseResponse
    {
        public IList<CategoryDTO> Data { get; set; }
    }
}
