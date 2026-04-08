using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Features.Permissions;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
  public void Configure(EntityTypeBuilder<Permission> builder)
  {
    builder.ToTable("Permissions");
    builder.HasKey(p => p.Id);

    builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
    builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
    builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate").IsRequired(false);

    builder.Property(p => p.Name)
      .HasMaxLength(100)
      .IsRequired();

    builder.Property(p => p.Description)
      .HasMaxLength(250)
      .IsRequired(false);

    builder.HasIndex(p => p.Name).IsUnique();
  }
}
