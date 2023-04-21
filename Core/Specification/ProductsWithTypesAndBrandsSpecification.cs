using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productSpecParams)
            : base(x =>
                (string.IsNullOrEmpty(productSpecParams.Search) ||  x.Name.ToLower().Contains(productSpecParams.Search)) &&
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId)
            )
        {
            AddInclude(x => x.productType);
            AddInclude(x => x.productBrand);
            AddOrderBy(x => x.Name);

            ApplyPagination(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);



            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.price);
                        break;

                    case "priceDesc":
                        AddOrderByDesc(x => x.price);
                        break;

                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }

            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
                : base(x => x.Id == id)
        {
            AddInclude(x => x.productType);
            AddInclude(x => x.productBrand);
        }
    }
}