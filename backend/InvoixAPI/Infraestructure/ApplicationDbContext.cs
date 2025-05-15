using Microsoft.EntityFrameworkCore;
using InvoixAPI.Domain;

namespace Invoix.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Client).IsRequired().HasMaxLength(100);
            entity.HasMany(i => i.Details)
                  .WithOne()
                  .HasForeignKey(d => d.InvoiceId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Product).IsRequired().HasMaxLength(100);
        });
    }
}
