using Solution.Application.Contracts.Persistence;
using Solution.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICategoryRepository CategoryRepository =>
            _categoryRepository ??= new CategoryRepository(_dbContext);

        public IProductRepository ProductRepository =>
            _productRepository ??= new ProductRepository(_dbContext);


        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
