
using AutoMapper;

using Store.data.Entity;
using Store.Repository.Interface;
using Store.Repository.Specification.ProductSpecification;
using Store.Service.Helpers;
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

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductAsync(ProductSpecifications input)
        {
            var countSpecs = new ProductCountWithSpecifiactions(input);
            var specs = new ProductWithSpecifiactions(input);
            var productsCount = await _unitOfWork.Repository<Product, int>().GetCountWithSpecificationAsync(countSpecs);
            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecificationAsync(specs);
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);

            return new PaginatedResultDto<ProductDetailsDto>(input.PageIndex , input.PageSize, productsCount , mappedProducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypeAsync()
        {
           var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            var mappedTypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);
            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? productId)
        {
            var specs = new ProductWithSpecifiactions(productId);
            var products = await _unitOfWork.Repository<Product, int>().GetWithSpecificationByIdAsync(specs);
            var mappedProducts = _mapper.Map<ProductDetailsDto>(products);
            return mappedProducts;
        }
    }
}
