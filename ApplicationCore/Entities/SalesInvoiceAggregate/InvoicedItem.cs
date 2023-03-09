using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.SalesInvoiceAggregate
{
    public class InvoicedItem // Value Object
    {
        public Guid ItemId { get; private set; }

        public string ItemName { get; private set; } = string.Empty;

        private InvoicedItem()
        {
            // Required by EF
        }

        public InvoicedItem(Guid itemId, string itemName)
        {
            Guard.AgainstNull(itemId, nameof(itemId));
            Guard.AgainstNullOrEmpty(itemName, nameof(itemName));

            ItemId = itemId;
            ItemName = itemName;
        }
    }
}
