using Solution.Application.Common;
using Solution.Application.Features.Products.DTOs;

namespace Solution.Application.Features.Products.Responses
{
    public class SearchProductsResponse : BaseResponse
    {
        public IList<ProductDTO> Data { get; set; }
    }
}
