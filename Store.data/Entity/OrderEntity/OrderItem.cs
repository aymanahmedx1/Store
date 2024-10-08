namespace Store.data.Entity.OrderEntity
{
	public class OrderItem : BaseEntity<Guid>
	{
		public decimal price { get; set; }
		public int Quantity { get; set; }
		public Guid OrderId { get; set; }
		public ProductItem ProductItem { get; set; }
	}
}
