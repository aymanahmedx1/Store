using Store.data.Entity.OrderEntity;

namespace Store.Repository.Specification.OrderSpecification
{
    public class OrderWithIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderWithIntentIdSpecification(string? intentId) : base(x => x.PaymentIntentId == intentId)
        {
        }
    }
}
