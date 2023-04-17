using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductByIdAsync(int Id)
        {
            return await _context.Products
            .Include(p => p.productType)
                .Include(p => p.productBrand)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsListAsync()
        {
            return await _context.Products
                .Include(p => p.productType)
                .Include(p => p.productBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            return await _context.ProductBrand.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypeAsync()
        {
            return await _context.ProductType.ToListAsync();
        }

    }
}