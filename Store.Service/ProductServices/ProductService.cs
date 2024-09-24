
using AutoMapper;

using Store.data.Entity;
using Store.Repository.Interface;
using Store.Service.ProductServices.Dto;

namespace Store.Service.ProductServices
{
    public class ProductService(IMapper _mapper, IUnitOfWork _unitOfWork) : IProductService
    {
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();
            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return mappedBrands;
        }

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllProductAsync()
        {
            var products = await _unitOfWork.Repository<Product, int>().GetAllAsync();
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);
            return mappedProducts;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypeAsync()
        {
           var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            var mappedTypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);
            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? productId)
        {
            var products = await _unitOfWork.Repository<Product, int>().GetByIdAsync(productId.Value);
            var mappedProducts = _mapper.Map<ProductDetailsDto>(products);
            return mappedProducts;
        }
    }
}
