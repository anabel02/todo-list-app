using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListApp.Domain;

namespace ToDoListApp.Persistence.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.UserId)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(p => p.UserId)
            .IsUnique();
    }
}