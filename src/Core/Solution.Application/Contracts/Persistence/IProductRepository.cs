using Solution.Application.Features.Products.Requests;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Application.Contracts.Persistence
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<IQueryable<Product>> GetProductsAsQueryableAsync();
    }
}
