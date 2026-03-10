using Core.AggregateRoots;
using Core.Common;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Types;

namespace Infrastructure.Persistence.Configurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");
        
        builder.HasKey(c => c.Id)
            .HasName("client_id");

        builder.OwnsOne(
            c => c.ClientCnpj,
            cnpj =>
            {
                cnpj.Property(c => c.Value)
                    .HasColumnName("cnpj")
                    .HasColumnType("varchar(14)")
                    .HasMaxLength(14)
                    .IsRequired();
                
                cnpj.HasIndex(c => c.Value).IsUnique();
            });
        
        builder.OwnsOne(
                c => c.ClientName,
                name => name.Property(c => c.Value)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasMaxLength(255)
                    .IsRequired());
        
        builder.Property(c => c.Status)
            .HasConversion(
                status => status.Id,
                id => Enumeration.FromId<ClientStatus>(id))
            .HasColumnName("status")
            .HasColumnType("integer")
            .IsRequired();

        builder.Property<DateTime>("created_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();
        
        builder.Property<DateTime?>("updated_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired(false);
        
        builder.Property(c => c.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired(false);
    }
}