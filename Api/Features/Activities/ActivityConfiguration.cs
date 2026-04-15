using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Features.Activities;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
  public void Configure(EntityTypeBuilder<Activity> builder)
  {
    builder.ToTable("Activities");

    builder.HasKey(a => a.Id);

    builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
    builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
    builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate").IsRequired(false);

    builder.Property(a => a.Action).HasMaxLength(50).IsRequired();
    builder.Property(a => a.EntityName).HasMaxLength(100).IsRequired();
    builder.Property(a => a.EntityId).HasMaxLength(50).IsRequired(false);
    builder.Property(a => a.IPAddress).HasMaxLength(50).IsRequired(false);

    builder.Property(a => a.OldValues).IsRequired(false);
    builder.Property(a => a.NewValues).IsRequired(false);

    builder.HasOne(a => a.User)
      .WithMany(u => u.Activities)
      .HasForeignKey(a => a.UserId)
      .OnDelete(DeleteBehavior.SetNull);
  }
}
