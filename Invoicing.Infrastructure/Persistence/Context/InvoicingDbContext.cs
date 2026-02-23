using Invoicing.Domain.Entities;
using Invoicing.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoicing.Infrastructure.Persistence.Context;

public class InvoicingDbContext : DbContext
{
    public InvoicingDbContext(DbContextOptions<InvoicingDbContext> options) : base(options) { }

    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<PricingConfigurationEntity> PricingConfigurations => Set<PricingConfigurationEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.OwnsOne(e => e.Client, client =>
            {
                client.Property(c => c.DocNumber).HasMaxLength(15).IsRequired();
                client.Property(c => c.FirstName).HasMaxLength(100).IsRequired();
                client.Property(c => c.LastName).HasMaxLength(100).IsRequired();
                client.Property(c => c.Address).HasMaxLength(200).IsRequired();
                client.Property(c => c.Phone).HasMaxLength(15).IsRequired();
            });

            entity.Property(e => e.TotalValue).HasPrecision(18, 2);
            entity.Property(e => e.Tax).HasPrecision(18, 2);
            entity.Property(e => e.GrossValue).HasPrecision(18, 2);
            entity.Property(e => e.Discount).HasPrecision(18, 2);

            entity.HasMany(e => e.Items)
                .WithOne()
                .HasForeignKey("InvoiceId")
                .OnDelete(DeleteBehavior.Cascade);

            entity.Metadata.FindNavigation(nameof(Invoice.Items))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);

            entity.Ignore(e => e.Total);
        });

        // Configuración de PricingConfiguration (tabla de infraestructura)
        modelBuilder.Entity<PricingConfigurationEntity>(entity =>
        {
            entity.ToTable("PricingConfigurations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TaxRate).HasPrecision(5, 4).IsRequired();
            entity.Property(e => e.DiscountRate).HasPrecision(5, 4).IsRequired();
            entity.Property(e => e.DiscountThreshold).HasPrecision(18, 2).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();

            // Seed data inicial según la prueba técnica
            entity.HasData(new PricingConfigurationEntity
            {
                Id = 1,
                TaxRate = 0.19m,           // 19% IVA
                DiscountRate = 0.05m,       // 5% descuento
                DiscountThreshold = 500000m, // $500,000 umbral
                CreatedAt = new DateTime(2024, 1, 1)
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}