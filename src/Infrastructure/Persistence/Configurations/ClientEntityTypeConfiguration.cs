using Core.AggregateRoots;
using Core.Enumerations;
using Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");
        
        builder.HasKey(c => c.Id)
            .HasName("client_id");

        builder.OwnsOne(
            c => c.Cnpj,
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
                c => c.Name,
                name => name.Property(c => c.Value)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .HasMaxLength(255)
                    .IsRequired());
        
        builder.Property(c => c.Status)
            .HasConversion(
                v => v.Id,
                v => (ClientStatus)Enum.Parse(typeof(ClientStatus), v.ToString()))
            .HasColumnName("status")
            .HasColumnType("integer")
            .IsRequired();

        builder.Property<DateTime>("created_at")
            .HasColumnType("timestamp")
            .IsRequired();
        
        builder.Property<DateTime?>("updated_at")
            .HasColumnType("timestamp")
            .IsRequired(false);
        
        builder.Property(c => c.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("timestamp")
            .IsRequired(false);
    }
}