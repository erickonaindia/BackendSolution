using Solution.Application.Common;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Application.Features.Categories.DTOs
{
    public class CategoryDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IList<Product> Products { get; set; }
    }
}
