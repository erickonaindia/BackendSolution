using Microsoft.EntityFrameworkCore;
using Solution.Application.Contracts.Persistence;
using Solution.Application.Features.Products.Requests;
using Solution.Domain.Entities;
using Solution.Persistence.Contexts;

namespace Solution.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IQueryable<Product>> GetProductsAsQueryableAsync()
        {
            return _dbContext.Products.AsQueryable();
        }
    }
}
