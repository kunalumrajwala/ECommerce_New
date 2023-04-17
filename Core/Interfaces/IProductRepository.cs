using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int Id);

        Task<IReadOnlyList<Product>> GetProductsListAsync();

        Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();

        Task<IReadOnlyList<ProductType>> GetProductTypeAsync();
    }
}