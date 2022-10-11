using Library.Course.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Course.Infrastructure.Data.EntityConfigurations;

public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<CourseItem>
{
    public void Configure(EntityTypeBuilder<CourseItem> builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        builder.ToTable("CourseItem");

        builder
            .HasKey(c => c.Id)
            .IsClustered(false)
            .HasName("PK_CourseItem");

        builder.Property(c => c.Name)
            .HasColumnType("nvarchar(150)")
            .IsRequired(true);
    }
}