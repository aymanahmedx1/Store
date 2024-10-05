namespace Store.Service.BasketService.Dtos
{
    public class CustomerBasketDto
    {
        public string? Id { get; set; }
        public string DeliveryMethodId { get; set; }
        public int ShippingPrice { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
    }
}
