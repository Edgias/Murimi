using MediatR;
using Microsoft.EntityFrameworkCore;
using NigTech.Murimi.ApplicationCore.Entities;
using NigTech.Murimi.ApplicationCore.Entities.CropProductionAggregate;
using NigTech.Murimi.ApplicationCore.Entities.PurchaseInvoiceAggregate;
using NigTech.Murimi.ApplicationCore.Entities.QuotationAggregate;
using NigTech.Murimi.ApplicationCore.Entities.SalesInvoiceAggregate;
using NigTech.Murimi.ApplicationCore.Entities.SalesOrderAggregate;
using NigTech.Murimi.ApplicationCore.SharedKernel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Murimi.Infrastructure.Data
{
    public class MurimiDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public MurimiDbContext(DbContextOptions<MurimiDbContext> options, IMediator mediator)
            :base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Asset> Assets { get; set; }

        public DbSet<Bin> Bins { get; set; }

        public DbSet<BinType> BinTypes { get; set; }

        public DbSet<Crop> Crops { get; set; }

        public DbSet<CropCategory> CropCategories { get; set; }

        public DbSet<CropProduction> CropProductions { get; set; }

        public DbSet<CropProductionField> CropProductionFields { get; set; }

        public DbSet<CropProductionVariety> CropProductionVarieties { get; set; }

        public DbSet<CropUnit> CropUnits { get; set; }

        public DbSet<CropVariety> CropVarieties { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Corporate> Corporates => Set<Corporate>();

        public DbSet<Field> Fields { get; set; }

        public DbSet<FieldMeasurement> FieldMeasurements { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Machinery> Machineries { get; set; }

        public DbSet<MachineryCategory> MachineryCategories { get; set; }

        public DbSet<NumberSequence> NumberSequences { get; set; }

        public DbSet<OwnershipType> OwnershipTypes { get; set; }

        public DbSet<PriceList> PriceLists { get; set; }

        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }

        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }

        public DbSet<Quotation> Quotations { get; set; }

        public DbSet<QuotationItem> QuotationItems { get; set; }

        public DbSet<SalesInvoice> SalesInvoices { get; set; }

        public DbSet<SalesInvoiceItem> SalesInvoiceItems { get; set; }

        public DbSet<SalesInvoiceNote> SalesInvoiceNotes { get; set; }

        public DbSet<SalesOrder> SalesOrders { get; set; }

        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<SeasonStatus> SeasonStatuses { get; set; }

        public DbSet<SoilType> SoilTypes { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Tax> Taxes { get; set; }

        public DbSet<UnitGroup> UnitGroups { get; set; }

        public DbSet<UnitMeasurement> UnitMeasurements { get; set; }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }

        public DbSet<WorkItemCategory> WorkItemCategories { get; set; }

        public DbSet<WorkItemStatus> WorkItemStatuses { get; set; }

        public DbSet<WorkItemSubCategory> WorkItemSubCategories { get; set; }

        public DbSet<YieldMeasurement> YieldMeasurements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_mediator == null) return result;

            // dispatch events only if save was successful
            BaseEntity[] entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (BaseEntity entity in entitiesWithEvents)
            {
                BaseDomainEvent[] events = entity.Events.ToArray();
                entity.Events.Clear();

                foreach (BaseDomainEvent domainEvent in events)
                {
                    await _mediator.Publish(domainEvent, cancellationToken).ConfigureAwait(false);
                }
            }

            return result;

        }
    }
}
