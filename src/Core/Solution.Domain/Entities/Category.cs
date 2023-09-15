using Solution.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual IList<Product> Products { get; set; }
    }
}
