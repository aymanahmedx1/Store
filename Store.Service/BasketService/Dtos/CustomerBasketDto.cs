namespace Store.Service.BasketService.Dtos
{
    public class CustomerBasketDto
    {
        public string? Id { get; set; }
        public int  DeliveryMethodId { get; set; }
        public int ShippingPrice { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
