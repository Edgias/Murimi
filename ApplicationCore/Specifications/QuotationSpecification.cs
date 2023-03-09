using NigTech.Murimi.ApplicationCore.Entities.QuotationAggregate;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class QuotationSpecification : BaseSpecification<Quotation>
    {
        public QuotationSpecification()
            : base(criteria: null!)
        {
            AddInclude(q => q.Contact!);
            AddInclude(q => q.Corporate!);
            ApplyOrderByDescending(q => q.QuotationDate);
        }

        public QuotationSpecification(Guid quotationId)
            : base(q => q.Id == quotationId)
        {
            AddInclude(q => q.Contact!);
            AddInclude(q => q.Corporate!);
            AddInclude(o => o.QuotationItems);
            AddInclude($"{nameof(Quotation.QuotationItems)}.{nameof(QuotationItem.ItemQuoted)}");
            AddInclude($"{nameof(Quotation.QuotationItems)}.{nameof(QuotationItem.Tax)}");
            ApplyOrderByDescending(q => q.QuotationDate);
        }

        public QuotationSpecification(Guid? customerId)
            : base(q => q.ContactId == customerId || q.CorporateId == customerId)
        {
            ApplyOrderByDescending(q => q.QuotationDate);
        }

    }
}
