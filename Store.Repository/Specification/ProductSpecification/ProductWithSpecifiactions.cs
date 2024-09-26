using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Store.data.Entity;

namespace Store.Repository.Specification.ProductSpecification
{
    public class ProductWithSpecifiactions : BaseSpecification<Product>
    {
        public ProductWithSpecifiactions(ProductSpecifications specs)
            : base(
                 product => (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value)
                        && (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value)
                        && (string.IsNullOrEmpty(specs.Search) || product.Name.ToLower().Contains(specs.Search))
                 )
        {
            AddInclud(p => p.Brand);
            AddInclud(p => p.Type);
            AddOrderBy(p => p.Name);

            ApplyPagination(specs.PageSize * (specs.PageIndex-1) , specs.PageSize);

            if (!string.IsNullOrEmpty(specs.Sort))
            {
                switch (specs.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        break;
                }
            }
        }
        public ProductWithSpecifiactions(int? id) : base(product => product.Id == id)
        {
            AddInclud(p => p.Brand);
            AddInclud(p => p.Type);
        }
    }
}
