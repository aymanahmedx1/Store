using Store.Service.ProductServices.Dto;

namespace Store.Service.ProductServices
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductByIdAsync(int? productId);
        Task<IReadOnlyList<ProductDetailsDto>> GetAllProductAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypeAsync();
    }
}
