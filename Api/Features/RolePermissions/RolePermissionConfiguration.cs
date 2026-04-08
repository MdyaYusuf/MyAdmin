using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Features.RolePermissions;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
  public void Configure(EntityTypeBuilder<RolePermission> builder)
  {
    builder.ToTable("RolePermissions");
    builder.HasKey(rp => rp.Id);

    builder.Property(rp => rp.Id).HasColumnName("Id").IsRequired();
    builder.Property(rp => rp.CreatedDate).HasColumnName("CreatedDate").IsRequired();
    builder.Property(rp => rp.UpdatedDate).HasColumnName("UpdatedDate").IsRequired(false);

    builder.Property(rp => rp.RoleId).IsRequired();
    builder.Property(rp => rp.PermissionId).IsRequired();

    builder.HasOne(rp => rp.Role)
      .WithMany(r => r.RolePermissions)
      .HasForeignKey(rp => rp.RoleId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(rp => rp.Permission)
      .WithMany(p => p.RolePermissions)
      .HasForeignKey(rp => rp.PermissionId)
      .OnDelete(DeleteBehavior.ClientCascade);
  }
}
