using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Store.Repository.Specification.ProductSpecification;
using Store.Service.ProductServices;
using Store.Service.ProductServices.Dto;
using Store.WebAPI.Helpers;

namespace Store.WebAPI.Controllers
{
	[Authorize]
	[Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController(IProductService _productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllBrands()
        => Ok(await _productService.GetAllBrandAsync());
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
       => Ok(await _productService.GetAllTypeAsync());

        [HttpGet]
        [Cash(100)]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllProducts([FromQuery]ProductSpecifications input)
            => Ok(await _productService.GetAllProductAsync(input));
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetProductById(int? id)
    => Ok(await _productService.GetProductByIdAsync(id));
    }
}
