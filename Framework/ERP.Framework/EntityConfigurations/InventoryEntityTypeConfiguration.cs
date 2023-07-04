using ERP.Domain.AggregatesModel.InventoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Framework.EntityConfigurations;

public class InventoryEntityTypeConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("inventories", InventoryContext.DEFAULT_SCHEMA);

        builder.HasKey(i => i.Id);

        builder.Ignore(b => b.DomainEvents);

        builder.Property(i => i.Id)
            .UseHiLo("inventoryseq", InventoryContext.DEFAULT_SCHEMA);

        builder
            .Property<DateTime>("_createdDate")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("CreatedDate")
            .IsRequired();

        builder
            .Property<int>("_inventoryStatusId")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("InventoryStatusId")
            .IsRequired();

        builder.Property<string>("Description").IsRequired(false);

        builder.HasOne(i => i.InventoryStatus)
            .WithMany()
            .HasForeignKey("_invetoryStatusId");
    }
}