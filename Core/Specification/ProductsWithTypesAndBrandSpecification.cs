using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandSpecification(ProductSpecParams productParams)
            : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
            (productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex -1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;

                }

            }
        }

        public ProductsWithTypesAndBrandSpecification(int id) : base(x =>x.Id ==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}