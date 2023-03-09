using NigTech.Murimi.ApplicationCore.Entities.SalesOrderAggregate;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class SalesOrderSpecification : BaseSpecification<SalesOrder>
    {
        public SalesOrderSpecification(Guid salesOrderId)
            : base(so => so.Id == salesOrderId)
        {
            AddInclude(o => o.SalesOrderItems);
            AddInclude($"{nameof(SalesOrder.SalesOrderItems)}.{nameof(SalesOrderItem.ItemOrdered)}");
        }

        public SalesOrderSpecification(Guid? customerId)
            : base(so => so.ContactId == customerId || so.CorporateId == customerId)
        {
            ApplyOrderByDescending(so => so.OrderDate);
        }

    }
}
