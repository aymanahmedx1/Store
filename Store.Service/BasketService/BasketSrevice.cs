using AutoMapper;

using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Service.BasketService.Dtos;

namespace Store.Service.BasketService
{
    public class BasketSrevice (IBasketRepository _basketRepository , IMapper _mapper) : IBasketSrevice
    {
       
        public async Task<bool> DeleteBasketAsync(string BasketId)
        => await _basketRepository.DeleteBasketAsync(BasketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string BasketId)
        {
            var basket = await _basketRepository.GetBasketAsync(BasketId);
            if (basket == null)
                return new CustomerBasketDto();
            var mappedBasket = _mapper.Map<CustomerBasketDto>(basket);
            return mappedBasket;    
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto input)
        {
            if (input.Id is null)
                input.Id = await GenerateBasketId();

            var basket = _mapper.Map<CustomerBasket>(input);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);
            var mappedBasket = _mapper.Map<CustomerBasketDto>(updatedBasket);
            return mappedBasket; 
        }
        private async Task<string> GenerateBasketId() { 
        
            Random rnd = new Random();  
            int random = rnd.Next(1000, 10000);
            var generatedKey = $"BS-{random}";
            return generatedKey;
        }
    }
}
