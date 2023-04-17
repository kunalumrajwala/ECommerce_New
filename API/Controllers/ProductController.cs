using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(await _productRepository.GetProductsListAsync());
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<Product>> GetProductById(int Id)
        {
            return Ok(await _productRepository.GetProductByIdAsync(Id));
        }

        [HttpGet]
        [Route("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            return Ok(await _productRepository.GetProductBrandAsync());
        }

        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<ProductType>> GetProductType()
        {
            return Ok(await _productRepository.GetProductTypeAsync());
        }
    }
}