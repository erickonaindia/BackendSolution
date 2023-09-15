using Solution.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Application.Features.Products.DTOs
{
    public class ProductDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string CategoryCode { get; set; }
    }
}
