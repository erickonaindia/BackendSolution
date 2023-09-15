using MediatR;
using Solution.Application.Features.Products.Responses;

namespace Solution.Application.Features.Products.Requests
{
    public class SearchCategoriesRequest : IRequest<SearchCategoriesResponse>
    {
        public string ProductCode { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortBy { get; set; }
        public bool Descending { get; set; }

    }
}