using Solution.Application.Features.Products.Requests;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<IQueryable<Category>> GetCategoriesAsQueryableAsync();

        Task<Category> GetCategoryByName(string categoryName);
    }
}
