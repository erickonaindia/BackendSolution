using AutoMapper;
using FluentValidation;
using MediatR;
using Solution.Application.Contracts.Persistence;
using Solution.Application.Features.Categories.DTOs;
using Solution.Application.Features.Categories.Validators;
using Solution.Application.Features.Products.Requests;
using Solution.Application.Features.Products.Responses;
using Solution.Domain.Entities;

namespace Solution.Application.Features.Categories.Handlers.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        static Random random = new Random();

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateCategoryDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CreateCategoryDTO);
            var response = new CreateCategoryResponse();
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

                return response;
            }

            var existingEvent = await _unitOfWork.CategoryRepository.GetCategoryByName(request.CreateCategoryDTO.Name);
            if (existingEvent != null)
            {
                response.Success = false;
                response.Message = "Category with a similar name already exists.";
                response.Errors = new List<string>() { "Category with a similar name already exists." };

                return response;
            }
            var category = _mapper.Map<Category>(request.CreateCategoryDTO);
            category.Id = new Guid();
            category.Code = request.CreateCategoryDTO.Code;
            category.Name = request.CreateCategoryDTO.Name;
            category = await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.Save();
            response.Success = true;
            response.Message = "Created Successfully.";
            response.Data = _mapper.Map<CreateCategoryDTO>(category);
            return response;
        }

    }
}
