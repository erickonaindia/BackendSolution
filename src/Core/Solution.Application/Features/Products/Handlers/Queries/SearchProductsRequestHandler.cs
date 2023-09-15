using AutoMapper;
using MediatR;
using Solution.Application.Contracts.Persistence;
using Solution.Application.Features.Products.DTOs;
using Solution.Application.Features.Products.Requests;
using Solution.Application.Features.Products.Responses;

namespace Solution.Application.Features.Products.Handlers.Queries
{
    public class SearchProductsRequestHandler : IRequestHandler<SearchProductsRequest, SearchProductsResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchProductsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SearchProductsResponse> Handle(SearchProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetProductsAsQueryableAsync();

            if (!string.IsNullOrEmpty(request.ProductCode))
                products = products.Where(p => p.Code.Contains(request.ProductCode));

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                if (request.Descending)
                    products = products.OrderByDescending(x=>x.Name);
                else
                    products = products.OrderBy(x => x.Name);
            }

            products = products.Skip((request.Page - 1) * request.PageSize)
                         .Take(request.PageSize);

            var response = new SearchProductsResponse();
            if (products == null)
            {
                response.Success = false;
                response.Message = "Product Not Found.";
                return response;
            }
            response.Success = true;
            response.Data = products.Select(x => new ProductDTO
            {
                CategoryCode = x.CategoryCode,
                Code = x.Code,
                Created = x.Created,
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return response;
        }
    }
}
