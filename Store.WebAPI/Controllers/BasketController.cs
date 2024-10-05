using Microsoft.AspNetCore.Mvc;

using Store.Service.BasketService;
using Store.Service.BasketService.Dtos;

namespace Store.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController (IBasketSrevice _basketSrevice) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketAsync(string basketId) 
          =>  Ok(await _basketSrevice.GetBasketAsync(basketId));
        

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto input)
        => Ok(await _basketSrevice.UpdateBasketAsync(input));

        [HttpDelete]
        public async Task<ActionResult<CustomerBasketDto>> DeleteBasketAsync(string basketId)
       => Ok(await _basketSrevice.DeleteBasketAsync(basketId));

    }
}
