using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Store.data.Entity.OrderEntity;

namespace Store.Repository.Specification.OrderSpecification
{
    public class OrderWithItemSpecification : BaseSpecification<Order>
    {
        public OrderWithItemSpecification(string buyerEmail) 
            : base(o=>o.BuyerEmail == buyerEmail)
        {
            AddInclud(x => x.DeliveryMethod);
            AddInclud(x => x.OrderItems);
            AddOrderByDescending(x => x.OrderDate);
        }
        public OrderWithItemSpecification(Guid id)
           : base(o => o.Id == id)
        {
            AddInclud(x => x.DeliveryMethod);
            AddInclud(x => x.OrderItems);
        }
    }
}
