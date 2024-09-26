using Store.Repository.Specification.ProductSpecification;
using Store.Service.Helpers;
using Store.Service.ProductServices.Dto;

namespace Store.Service.ProductServices
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int? productId);
        Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductAsync(ProductSpecifications input);
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypeAsync();
    }
}
