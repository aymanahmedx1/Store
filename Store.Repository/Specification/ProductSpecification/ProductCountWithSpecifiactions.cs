using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Store.data.Entity;

namespace Store.Repository.Specification.ProductSpecification
{
    public class ProductCountWithSpecifiactions : BaseSpecification<Product>
    {
        public ProductCountWithSpecifiactions(ProductSpecifications specs)
         : base(product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value)
                     && (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value)
                     && (string.IsNullOrEmpty(specs.Search) || product.Name.ToLower().Contains(specs.Search))
              )
        { }
    }
}
