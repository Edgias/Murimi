using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.PurchaseInvoiceAggregate
{
    public class InvoicedItem // ValueObject
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
