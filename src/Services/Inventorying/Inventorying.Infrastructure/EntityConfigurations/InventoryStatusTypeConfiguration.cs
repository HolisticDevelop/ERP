using ERP.Services.Inventorying.Domain.AggregatesModel.InventoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Services.Inventorying.Infrastructure.EntityConfigurations;

public class InventoryStatusTypeConfiguration : IEntityTypeConfiguration<InventoryStatus>
{
    public void Configure(EntityTypeBuilder<InventoryStatus> builder)
    {
        builder.ToTable("inventorystatus", InventoryContext.DEFAULT_SCHEMA);

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasDefaultValue(1)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(i => i.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}