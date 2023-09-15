using AutoMapper;
using MediatR;
using Solution.Application.Contracts.Persistence;
using Solution.Application.Features.Categories.DTOs;
using Solution.Application.Features.Products.DTOs;
using Solution.Application.Features.Products.Requests;
using Solution.Application.Features.Products.Responses;

namespace Solution.Application.Features.Products.Handlers.Queries
{
    public class SearchCategoriesRequestHandler : IRequestHandler<SearchCategoriesRequest, SearchCategoriesResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchCategoriesRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SearchCategoriesResponse> Handle(SearchCategoriesRequest request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsQueryableAsync();

            if (!string.IsNullOrEmpty(request.ProductCode))
                categories = categories.Where(p => p.Code.Contains(request.ProductCode));

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                if (request.Descending)
                    categories = categories.OrderByDescending(x=>x.Name);
                else
                    categories = categories.OrderBy(x => x.Name);
            }

            categories = categories.Skip((request.Page - 1) * request.PageSize)
                         .Take(request.PageSize);

            var response = new SearchCategoriesResponse();
            if (categories == null)
            {
                response.Success = false;
                response.Message = "Category Not Found.";
                return response;
            }
            response.Success = true;
            response.Data = categories.Select(x=> new CategoryDTO
            {
                Code = x.Code,
                Created = x.Created,
                Id = x.Id,
                Name = x.Name,
                Products = x.Products
            }).ToList();
            return response;
        }
    }
}
