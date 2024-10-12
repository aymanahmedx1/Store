namespace Store.Service.OrderServices.Dto
{
    public class OrderItemDto
    {
        public Guid OrderId { get; set; }
        public decimal price { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
    }
}
