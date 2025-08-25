using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListApp.Domain;

namespace ToDoListApp.Persistence.Configurations;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ToTable("ToDos");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Task)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.CreatedDateTime)
            .HasColumnType("datetime(6)");

        builder.Property(t => t.CompletedDateTime)
            .HasColumnType("datetime(6)")
            .IsRequired(false)
            .HasDefaultValue(null);

        builder
            .HasOne(t => t.Profile)
            .WithMany(p => p.ToDos)
            .HasForeignKey(t => t.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}