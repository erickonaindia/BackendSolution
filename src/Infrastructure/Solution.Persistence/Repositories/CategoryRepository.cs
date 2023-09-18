using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Solution.Application.Contracts.Persistence;
using Solution.Application.Features.Products.Requests;
using Solution.Domain.Entities;
using Solution.Persistence.Contexts;

namespace Solution.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IQueryable<Category>> GetCategoriesAsQueryableAsync()
        {
            return _dbContext.Categories
                .AsQueryable()
                .Include(x => x.Products);
        }

        public async Task<Category> GetCategoryByName(string categoryName)
        {
            return await (await GetQueryAsync(x => x.Name.ToLower() == categoryName.ToLower())).FirstOrDefaultAsync();
        }
    }
}
