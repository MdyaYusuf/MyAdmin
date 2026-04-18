using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Features.Notifications;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
  public void Configure(EntityTypeBuilder<Notification> builder)
  {
    builder.ToTable("Notifications");

    builder.HasKey(n => n.Id);

    builder.Property(n => n.Id).HasColumnName("Id").IsRequired();
    builder.Property(n => n.CreatedDate).HasColumnName("CreatedDate").IsRequired();

    builder.Property(n => n.UpdatedDate)
      .HasColumnName("ReadAt")
      .IsRequired(false);

    builder.Property(n => n.Title).HasMaxLength(200).IsRequired();

    builder.Property(n => n.Message).HasMaxLength(500).IsRequired();

    builder.Property(n => n.Type)
      .HasMaxLength(50)
      .IsRequired()
      .HasDefaultValue("INFO");

    builder.Property(n => n.LinkUrl).HasMaxLength(250).IsRequired(false);

    builder.HasOne(n => n.User)
      .WithMany(u => u.Notifications)
      .HasForeignKey(n => n.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
