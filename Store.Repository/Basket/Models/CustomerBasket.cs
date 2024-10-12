namespace Store.Repository.Basket.Models
{
    public class CustomerBasket
    {
        public string? Id { get; set; }
        public string DeliveryMethodId { get; set; }
        public int ShippingPrice { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

    }
}
