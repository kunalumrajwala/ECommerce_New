using API.Dtos;
using API.Error;
using API.Helper;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {

        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productRepo,
                                 IGenericRepository<ProductType> productTypeRepo,
                                 IGenericRepository<ProductBrand> productBrandRepo,
                                 IMapper mapper)
        {
            _productRepo = productRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productSpecParams);

            var countSpec = new ProductWithFilterForCountSpecification(productSpecParams);

            var totalItems = await _productRepo.CountAsync(countSpec);
            var result = await _productRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(result);

            return Ok(new Pagination<ProductToReturnDto>(productSpecParams.PageIndex, productSpecParams.PageSize, totalItems, data));
        }

        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int Id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(Id);
            var result = await _productRepo.GetEntityWithSpec(spec);

            if (result == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDto>(result);
        }

        [HttpGet]
        [Route("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<ProductType>> GetProductType()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }
    }
}