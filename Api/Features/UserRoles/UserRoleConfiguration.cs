using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Features.UserRoles;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
  public void Configure(EntityTypeBuilder<UserRole> builder)
  {
    builder.ToTable("UserRoles");
    builder.HasKey(ur => ur.Id);

    builder.Property(ur => ur.Id).HasColumnName("Id").IsRequired();
    builder.Property(ur => ur.CreatedDate).HasColumnName("CreatedDate").IsRequired();
    builder.Property(ur => ur.UpdatedDate).HasColumnName("UpdatedDate").IsRequired(false);

    builder.Property(ur => ur.UserId).IsRequired();
    builder.Property(ur => ur.RoleId).IsRequired();

    builder.HasOne(ur => ur.User)
      .WithMany(u => u.UserRoles)
      .HasForeignKey(ur => ur.UserId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(ur => ur.Role)
      .WithMany(r => r.UserRoles)
      .HasForeignKey(ur => ur.RoleId)
      .OnDelete(DeleteBehavior.ClientCascade);
  }
}
